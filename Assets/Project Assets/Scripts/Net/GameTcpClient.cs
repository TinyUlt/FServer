using UnityEngine;
using System.Collections;
using Google.Protobuf.Gt;
using Google.Protobuf;
using System.Net.Sockets;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Net;
public class GameTcpClient : MonoBehaviour {

	enum SocketState{
		None,           //无
		Connecting,     //链接中
		ConnectSuccess, //链接成功
		ConnectFail,    //链接失败
		ConnectEnd,     //链接完成
		SocketError,    //出现错误
		CloseConnect,   //关闭链接
	}

	private SocketState socketState;

	private Socket socketClient;

	private bool isConnect = false;

	//发包
	private System.Object m_sendObject;
	private ManualResetEvent m_sendEvent;
	private LinkedList<IMessage> m_SendPackList;

	//收包
	private System.Object m_receiveObject;
	private LinkedList<IMessage> m_ReceivePackList;

	private Action<bool> callbackOnConnect;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void init(string ip, string port, Action<bool> callback){
	
		callbackOnConnect = callback;

		IPEndPoint ipe =new IPEndPoint(IPAddress.Parse(ip), Convert.ToInt32(port));

		socketClient = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

		socketClient.BeginConnect(ipe,new AsyncCallback(OnSocketConnectResult),socketClient);
	}

	private void OnSocketConnectResult(IAsyncResult result){
		
		Socket s = (Socket)result.AsyncState;

		if (s.Connected) {
			
			isConnect = true;

			InitNetLoopThread();

			socketState = SocketState.ConnectSuccess;

			callbackOnConnect (true);

		} else {
			
			Debug.LogError ("连接服务器失败 ");

			socketState = SocketState.ConnectFail;

			callbackOnConnect (false);
		}
	}

	private void InitNetLoopThread(){
		Debug.Log ("[InitNetLoopThread]");
		m_SendPackList = new LinkedList<IMessage> ();
		m_sendObject = new object ();
		m_sendEvent = new ManualResetEvent (false);

		m_ReceivePackList = new LinkedList<IMessage> ();
		m_receiveObject = new object ();

		Thread receiveThread = new Thread (ReceiveLoopThread);
		receiveThread.Start ();
		Thread sendThread = new Thread (SendLoopThread);
		sendThread.Start ();

	}
	//收包轮循
	private void ReceiveLoopThread(){
		Debug.Log("ReceiveLoopThread start");
//		m_receiveHead = new byte[PACKET_HEAD_SIZE];//总长度 大id 小id
		while (socketClient != null && isConnect == true &&  socketClient.Connected) {
			try{
				if (socketClient.Available > 0) {
					Debug.Log("[socketClient.Available > 0]="+socketClient.Available);
//					if (m_receivehead == null && m_currentPacketLength == 0 && m_socket.Available > PACKET_HEAD_SIZE) { //接收包头
//						m_socket.Receive (m_receiveHead, PACKET_HEAD_SIZE, SocketFlags.None);
//						ByteBuffer buffer = new ByteBuffer (m_receiveHead);
//						m_receivehead = new TCP_Info();
//						m_receivehead.Deserialize(buffer);
//						m_currentPacketLength = m_receivehead.wPacketSize - PACKET_HEAD_SIZE; //总长度减去包头长度
//					}
//					if (m_receivehead != null && m_currentPacketLength > 0 /*&& m_socket.Available >= m_currentPacketLength*/) { //接收包
//						byte[] bodyBytes = new byte[m_currentPacketLength];
//						int receiveBuffSize = 0;
//						while(receiveBuffSize < m_currentPacketLength && m_isConnect == true){
//							int tempSize = m_socket.Receive (bodyBytes,receiveBuffSize, m_currentPacketLength - receiveBuffSize, SocketFlags.None);
//							receiveBuffSize += tempSize;
//							Thread.Sleep(1);
//						}
//						if(receiveBuffSize == m_currentPacketLength){
//							DeserializePacket(bodyBytes,m_receivehead);
//							m_currentPacketLength = 0;
//							m_receivehead = null;
//						}else{
//							Debug.LogError ("ReceiveLoopThread error 接收包长度错误");
//							m_needClose = true;
//						}
//						//m_socket.Receive (bodyBytes, m_currentPacketLength, SocketFlags.None);
//
//					}
//
				} else {
					Thread.Sleep (10);
				}
			}
			catch(Exception e){
//				Debug.LogError ("ReceiveLoopThread error "+e);
//				m_needClose = true;
			}
		}
		Debug.Log ("ReceiveLoopThread die");
	}

	//发包轮循
	private void SendLoopThread(){
		
		while (isConnect) {
			
			Queue<IMessage> sendQueue = new Queue<IMessage> ();

			lock (m_sendObject) {
				
				while (m_SendPackList.Count > 0) {
					
					sendQueue.Enqueue (m_SendPackList.First.Value);

					m_SendPackList.RemoveFirst ();
				}
			}
			while (sendQueue.Count > 0) {
				
				IMessage packet = null;

				packet = sendQueue.Dequeue();

				if (packet != null) {
					
					try{

						int msgId = GameMessagehandler.RequsetIds[packet.Descriptor.Name];

						var loads = packet.ToByteArray ();

						codeData (loads);

						byte[] mMagic =System.Text.Encoding.Default.GetBytes ( "GTV1" );//  "GTV1";

						var lenght = mMagic.Length + 2 + 2 + loads.Length;

						var result = new byte[lenght];

						for(var i = 0; i < mMagic.Length; i++){
							result[i] = mMagic[i];
						}

						byte[] twochar = new byte[2]	;
						twochar[0] = (byte)(msgId>>8);
						twochar[1] = (byte)(msgId>>0);

						for(var i = 0; i < 2; i++){
							result[i + mMagic.Length] = twochar[i];
						}

						twochar[0] = (byte)(loads.Length>>8);
						twochar[1] = (byte)(loads.Length>>0);

						for(var i = 0; i < 2 ; i++){
							result[i + mMagic.Length + 2] = twochar[i];
						}

						for(var i =0; i < loads.Length; i++){
							result[i + mMagic.Length + 2 + 2] = loads[i];
						}

						socketClient.Send(result,0,lenght,SocketFlags.None);
					}
					catch(Exception e){
						
						Debug.Log ("Send error.. "+e.ToString());
					}
				}
			}
			m_sendEvent.Reset ();

			m_sendEvent.WaitOne ();
		}
	}
	byte[] codeData( byte[] data )
	{
		byte []key = {69, 123, 132, 104, 67, 95, 33, 74, 120, 131, 61, 101, 55, 101, 69, 44};

		for (int i = 0; i< data.Length; i++)
		{

			data[i]^= key[(i) % key.Length];
		}
		return data;
	}
	public void SendPacket(IMessage packet){
		
		lock (m_sendObject) {
			
			m_SendPackList.AddLast (packet);
		}
		m_sendEvent.Set ();
	}



	/////////////////////
	/// 
	/// 
//	public sendLoginRequest(string ip ){
//		
//	}
}
