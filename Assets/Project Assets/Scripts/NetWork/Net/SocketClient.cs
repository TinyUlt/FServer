 using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Net;

public class SocketClient{
	enum SocketState{
		None,           //无
		Connecting,     //链接中
		ConnectSuccess, //链接成功
		ConnectFail,    //链接失败
		ConnectEnd,     //链接完成
		SocketError,    //出现错误
		CloseConnect,   //关闭链接
	}

	private const int PACKET_HEAD_SIZE =171; //长度 大id 小id

	private SocketState m_socketState;
	private Socket m_socket;
	private string m_remoteIP;
	private int m_portNum;

	private int m_socketType;

	private bool m_isConnect;
	private bool m_needClose;

	//发包
	private System.Object m_sendObject;
	private ManualResetEvent m_sendEvent;
	private LinkedList<NetPacket> m_SendPackList;

	//收包
	private System.Object m_receiveObject;
	private LinkedList<NetPacket> m_ReceivePackList;

	private Action<bool> m_callbackOnConnect;
	private Action<int> m_callbackOnDisConnect;

	private Action<int,NetPacket> m_callbackOnGetPacket;

	private byte[] m_receiveHead;

	public bool isConnect{get{ return m_isConnect;}}

	public SocketClient(int socketType){
		m_socketType = socketType;
		m_isConnect = false;
		m_socketState = SocketState.None;
	}

	public void OnRun(){
		
		if (m_socketState == SocketState.ConnectSuccess) {
			if (m_callbackOnConnect != null)
				m_callbackOnConnect (true);
			m_socketState = SocketState.ConnectEnd;
		} else if (m_socketState == SocketState.ConnectFail) {
			if (m_callbackOnConnect != null)
				m_callbackOnConnect (false);
			m_socketState = SocketState.ConnectEnd;
		} else if (m_socketState == SocketState.SocketError) {
			if (m_callbackOnDisConnect != null)
				m_callbackOnDisConnect (m_socketType);
			m_socketState = SocketState.ConnectEnd;
		}
		if (m_isConnect == false)
			return;
		if (m_needClose == true) {
			Close ();
			return;
		}

		if (m_ReceivePackList != null && m_ReceivePackList.Count != 0) {
			Queue<NetPacket> sendQueue = new Queue<NetPacket> ();
			lock (m_receiveObject) {
				while (m_ReceivePackList.Count > 0) {
					sendQueue.Enqueue (m_ReceivePackList.First.Value);
					m_ReceivePackList.RemoveFirst ();
				}
			}
			while(sendQueue.Count > 0){
				m_callbackOnGetPacket (m_socketType,sendQueue.Dequeue());
			}
		}
	}

	public void SetOnGetPacketCallback(Action<int,NetPacket> call){
		m_callbackOnGetPacket = call;
	}

	public void SetOnLostConnectCallback(Action<int> call){
		m_callbackOnDisConnect = call;
	}


	//
	public bool Connect(string remoteIP,int portNum,Action<bool> connectCallBack){
		m_callbackOnConnect = connectCallBack;

		if (m_isConnect) {
			Close ();
		}
		m_remoteIP = remoteIP;
		m_portNum = portNum;

		try{
			Debug.Log("连接服务器 " + m_remoteIP + " " + portNum);
			IPEndPoint ip =new IPEndPoint(IPAddress.Parse(m_remoteIP), m_portNum);
			m_socket = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			m_socket.BeginConnect(ip,new AsyncCallback(OnSocketConnectResult),m_socket);
			m_socketState = SocketState.Connecting;
		}
		catch(Exception e){
			Debug.LogError ("连接服务器失败 "+e);
			m_isConnect = false;
			m_socketState = SocketState.ConnectFail;
			return false;
		}
		return true;
	}

	private void OnSocketConnectResult(IAsyncResult result){
		Socket s = (Socket)result.AsyncState;
		if (s.Connected) {
			m_isConnect = true;
			InitNetLoopThread();
			m_socketState = SocketState.ConnectSuccess;
		} else {
			Debug.LogError ("连接服务器失败 ");
			m_socketState = SocketState.ConnectFail;
		}
	}

//
//	private void ThreadConnect(){
//		IPEndPoint ip =new IPEndPoint(IPAddress.Parse(m_remoteIP), m_portNum);
//		m_socket = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//		//m_socket.ReceiveBufferSize = ;
//		try{
//			m_socket.Connect(ip);
//			if(m_socket.Connected){
//				m_isConnect = true;
//			}
//		}
//		catch(Exception e){
//			Debug.Log (e);
//		}
//	}


	private void InitNetLoopThread(){
		Debug.Log ("[InitNetLoopThread]");
		m_SendPackList = new LinkedList<NetPacket> ();
		m_sendObject = new object ();
		m_sendEvent = new ManualResetEvent (false);

		m_ReceivePackList = new LinkedList<NetPacket> ();
		m_receiveObject = new object ();

		Thread receiveThread = new Thread (ReceiveLoopThread);
		receiveThread.Start ();
		Thread sendThread = new Thread (SendLoopThread);
		sendThread.Start ();

	}

	int m_currentPacketLength;
	//int m_currentPacketMajor;
	//int m_currentPacketMinor;
	TCP_Info m_receivehead = null;
	//收包轮循
	private void ReceiveLoopThread(){
        Debug.Log("ReceiveLoopThread start");
        m_receiveHead = new byte[PACKET_HEAD_SIZE];//总长度 大id 小id
		while (m_socket != null && m_isConnect == true && m_needClose == false && m_socket.Connected) {
			try{
				if (m_socket.Available > 0) {
					if (m_receivehead == null && m_currentPacketLength == 0 && m_socket.Available > PACKET_HEAD_SIZE) { //接收包头
						m_socket.Receive (m_receiveHead, PACKET_HEAD_SIZE, SocketFlags.None);
						ByteBuffer buffer = new ByteBuffer (m_receiveHead);
						m_receivehead = new TCP_Info();
						m_receivehead.Deserialize(buffer);
						m_currentPacketLength = m_receivehead.wPacketSize - PACKET_HEAD_SIZE; //总长度减去包头长度
					}
					if (m_receivehead != null && m_currentPacketLength > 0 /*&& m_socket.Available >= m_currentPacketLength*/) { //接收包
						byte[] bodyBytes = new byte[m_currentPacketLength];
						int receiveBuffSize = 0;
						while(receiveBuffSize < m_currentPacketLength && m_isConnect == true){
							int tempSize = m_socket.Receive (bodyBytes,receiveBuffSize, m_currentPacketLength - receiveBuffSize, SocketFlags.None);
							receiveBuffSize += tempSize;
							Thread.Sleep(1);
						}
						if(receiveBuffSize == m_currentPacketLength){
							DeserializePacket(bodyBytes,m_receivehead);
							m_currentPacketLength = 0;
							m_receivehead = null;
						}else{
							Debug.LogError ("ReceiveLoopThread error 接收包长度错误");
							m_needClose = true;
						}
						//m_socket.Receive (bodyBytes, m_currentPacketLength, SocketFlags.None);

					}

				} else {
					Thread.Sleep (10);
				}
			}
			catch(Exception e){
				Debug.LogError ("ReceiveLoopThread error "+e);
				m_needClose = true;
			}
		}
		Debug.Log ("ReceiveLoopThread die");
	}

	void DeserializePacket(byte[] bytes,TCP_Info head){
		if (!UnmapperBuffer (ref bytes, head)) {
			Debug.LogError ("UnmapperBuffer error");
			return;
		}
		//ByteBuffer buffer = new ByteBuffer (bytes);
		ushort MainCmdId = BitConverter.ToUInt16 (bytes, 0);
		ushort SubCmdId = BitConverter.ToUInt16 (bytes, 2);

		if (MainCmdId == 0 && SubCmdId == 1) {
			Debug.Log ("收到服务器心跳，发送心跳");
			SendHeatBeat ();
			return;
		}

		Type packetType = MessageCenter.GetInstance().GetMessageStruct((int)MainCmdId, (int)SubCmdId,(enSocketType)m_socketType);
		if (packetType == null)
		{
			Debug.LogError("客户端不存在消息对应的结构 " + MainCmdId + " " + SubCmdId);
			return;
		}
//		Debug.Log("收到消息结构: " + packetType + " MainCmdId: " + MainCmdId + " SubCmdId: " + SubCmdId);
		try
		{
			byte[] packetBytes = new byte[bytes.Length - 4];
			Array.Copy(bytes,4,packetBytes,0,packetBytes.Length);
			//bytes.CopyTo(packetBytes,4);
			NetPacket _tempStruct = (NetPacket)Activator.CreateInstance(packetType);
			_tempStruct.Deserialize(packetBytes);
			_tempStruct.mainCmd = MainCmdId;
			_tempStruct.subCmd = SubCmdId;
			_tempStruct.socketType = m_socketType;

			if (_tempStruct != null)
			{
				lock (m_receiveObject) {
					m_ReceivePackList.AddLast (_tempStruct);
				}
			}
		}
		catch(Exception e){
			Debug.LogError ("DeserializePacket error "+e);
		}
		
	}

	//发包轮循
	private void SendLoopThread(){
		while (m_isConnect) {
			Queue<NetPacket> sendQueue = new Queue<NetPacket> ();
			lock (m_sendObject) {
				while (m_SendPackList.Count > 0) {
					sendQueue.Enqueue (m_SendPackList.First.Value);
					m_SendPackList.RemoveFirst ();
				}
			}
			while (sendQueue.Count > 0) {
				NetPacket packet = null;
				packet = sendQueue.Dequeue();
				if (packet != null) {
					try{
						byte[] sendBuffer;
						CombinePacket(out sendBuffer,packet);
						Debug.Log("sendBuffer size "+sendBuffer.Length);
						m_socket.Send(sendBuffer,0,sendBuffer.Length,SocketFlags.None);
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

	//组合包头和包身
	void CombinePacket(out byte[] outBuffer,NetPacket packet){
		CombinePacket (out outBuffer, packet.Serialize (), (ushort)packet.mainCmd, (ushort)packet.subCmd);
	}

	//组合包头和包身
	void CombinePacket(out byte[] outBuffer,byte[] packetBuffer,ushort mainID,ushort subID){
		outBuffer = null;
		TCP_Info p_head = new TCP_Info ();

		//设置包头 (映射标记)
		p_head.cbDataKind = 0 ;
		p_head.cbCheckCode = 0;
		int totalSize = NetPacket.NetPacketSize(typeof(TCP_Info))+ 4 + packetBuffer.Length;
		p_head.wPacketSize = (ushort)totalSize;

		byte[] major = System.BitConverter.GetBytes((ushort)mainID);
		byte[] minor = System.BitConverter.GetBytes((ushort)subID);

		//字节拼装
		byte[] bodytotal = new byte[4+packetBuffer.Length];
		major.CopyTo(bodytotal,0);
		minor.CopyTo(bodytotal,2);
		packetBuffer.CopyTo (bodytotal, 4);
		if (MapperBuffer (ref bodytotal, p_head) == false) {
			return;
		}
		//这些数据直连都不需要 ？？？不设置值
//		p_head.netHead = new NetMessageHead();
//		p_head.netHead.uMessageBeginFlag = Define.BEGINFLAG;
//		p_head.netHead.dwPacketSize = p_head.wPacketSize;
//		p_head.netHead.dwCmdFlag = 2;
//		p_head.netHead.ulCmdNo = 0; //????
//		p_head.netHead.dwClientAddr = 0;//userid;

		outBuffer = new byte[totalSize];
		byte[] headBytes = p_head.Serialize ();
		headBytes.CopyTo (outBuffer,0);
		int headSize = NetPacket.NetPacketSize (typeof(TCP_Info));
		bodytotal.CopyTo (outBuffer,headSize );
	}

	bool MapperBuffer(ref byte[] buffer,TCP_Info head){
		byte cbCheckCode = 0;
		for (int i = 0; i < buffer.Length; i++) {
			cbCheckCode += buffer [i];
			buffer [i] = g_SendByteMap[buffer [i]];
		}
		head.cbDataKind |= Define.DK_MAPPED;
		head.cbCheckCode =  (byte)(~cbCheckCode + 1);
		return true;
	}

	bool UnmapperBuffer(ref byte[] buffer,TCP_Info head){
		if ((head.cbDataKind & Define.DK_MAPPED) != 0) {
			byte cbCheckCode = head.cbCheckCode;
			for (int i = 0; i < buffer.Length; i++) {
				cbCheckCode += g_RecvByteMap [buffer [i]];
				buffer[i] = g_RecvByteMap [buffer [i]];
			}
			if (cbCheckCode != 0)
				return false;
		}
		return true;
	}

	void SendHeatBeat(){
		CMD_GP_NoStruct packet = new CMD_GP_NoStruct ();
		packet.mainCmd = 0;
		packet.subCmd = 1;
		SendPacket (packet);
	}

	public void SendPacket(NetPacket packet){
		lock (m_sendObject) {
			m_SendPackList.AddLast (packet);
		}
		m_sendEvent.Set ();
	}

	public void Close(){
		m_isConnect = false;
//		m_socket.Shutdown (SocketShutdown.Both);
		m_socket.Close();
	}

	//发送映射
	static byte[] g_SendByteMap= new byte[256]
	{
	    0x70,0x2F,0x40,0x5F,0x44,0x8E,0x6E,0x45,0x7E,0xAB,0x2C,0x1F,0xB4,0xAC,0x9D,0x91,
	    0x0D,0x36,0x9B,0x0B,0xD4,0xC4,0x39,0x74,0xBF,0x23,0x16,0x14,0x06,0xEB,0x04,0x3E,
	    0x12,0x5C,0x8B,0xBC,0x61,0x63,0xF6,0xA5,0xE1,0x65,0xD8,0xF5,0x5A,0x07,0xF0,0x13,
	    0xF2,0x20,0x6B,0x4A,0x24,0x59,0x89,0x64,0xD7,0x42,0x6A,0x5E,0x3D,0x0A,0x77,0xE0,
	    0x80,0x27,0xB8,0xC5,0x8C,0x0E,0xFA,0x8A,0xD5,0x29,0x56,0x57,0x6C,0x53,0x67,0x41,
	    0xE8,0x00,0x1A,0xCE,0x86,0x83,0xB0,0x22,0x28,0x4D,0x3F,0x26,0x46,0x4F,0x6F,0x2B,
	    0x72,0x3A,0xF1,0x8D,0x97,0x95,0x49,0x84,0xE5,0xE3,0x79,0x8F,0x51,0x10,0xA8,0x82,
	    0xC6,0xDD,0xFF,0xFC,0xE4,0xCF,0xB3,0x09,0x5D,0xEA,0x9C,0x34,0xF9,0x17,0x9F,0xDA,
	    0x87,0xF8,0x15,0x05,0x3C,0xD3,0xA4,0x85,0x2E,0xFB,0xEE,0x47,0x3B,0xEF,0x37,0x7F,
	    0x93,0xAF,0x69,0x0C,0x71,0x31,0xDE,0x21,0x75,0xA0,0xAA,0xBA,0x7C,0x38,0x02,0xB7,
	    0x81,0x01,0xFD,0xE7,0x1D,0xCC,0xCD,0xBD,0x1B,0x7A,0x2A,0xAD,0x66,0xBE,0x55,0x33,
	    0x03,0xDB,0x88,0xB2,0x1E,0x4E,0xB9,0xE6,0xC2,0xF7,0xCB,0x7D,0xC9,0x62,0xC3,0xA6,
	    0xDC,0xA7,0x50,0xB5,0x4B,0x94,0xC0,0x92,0x4C,0x11,0x5B,0x78,0xD9,0xB1,0xED,0x19,
	    0xE9,0xA1,0x1C,0xB6,0x32,0x99,0xA3,0x76,0x9E,0x7B,0x6D,0x9A,0x30,0xD6,0xA9,0x25,
	    0xC7,0xAE,0x96,0x35,0xD0,0xBB,0xD2,0xC8,0xA2,0x08,0xF3,0xD1,0x73,0xF4,0x48,0x2D,
	    0x90,0xCA,0xE2,0x58,0xC1,0x18,0x52,0xFE,0xDF,0x68,0x98,0x54,0xEC,0x60,0x43,0x0F
	};

	//接收映射
	static byte[] g_RecvByteMap= new byte[256]
	{
	    0x51,0xA1,0x9E,0xB0,0x1E,0x83,0x1C,0x2D,0xE9,0x77,0x3D,0x13,0x93,0x10,0x45,0xFF,
	    0x6D,0xC9,0x20,0x2F,0x1B,0x82,0x1A,0x7D,0xF5,0xCF,0x52,0xA8,0xD2,0xA4,0xB4,0x0B,
	    0x31,0x97,0x57,0x19,0x34,0xDF,0x5B,0x41,0x58,0x49,0xAA,0x5F,0x0A,0xEF,0x88,0x01,
	    0xDC,0x95,0xD4,0xAF,0x7B,0xE3,0x11,0x8E,0x9D,0x16,0x61,0x8C,0x84,0x3C,0x1F,0x5A,
	    0x02,0x4F,0x39,0xFE,0x04,0x07,0x5C,0x8B,0xEE,0x66,0x33,0xC4,0xC8,0x59,0xB5,0x5D,
	    0xC2,0x6C,0xF6,0x4D,0xFB,0xAE,0x4A,0x4B,0xF3,0x35,0x2C,0xCA,0x21,0x78,0x3B,0x03,
	    0xFD,0x24,0xBD,0x25,0x37,0x29,0xAC,0x4E,0xF9,0x92,0x3A,0x32,0x4C,0xDA,0x06,0x5E,
	    0x00,0x94,0x60,0xEC,0x17,0x98,0xD7,0x3E,0xCB,0x6A,0xA9,0xD9,0x9C,0xBB,0x08,0x8F,
	    0x40,0xA0,0x6F,0x55,0x67,0x87,0x54,0x80,0xB2,0x36,0x47,0x22,0x44,0x63,0x05,0x6B,
	    0xF0,0x0F,0xC7,0x90,0xC5,0x65,0xE2,0x64,0xFA,0xD5,0xDB,0x12,0x7A,0x0E,0xD8,0x7E,
	    0x99,0xD1,0xE8,0xD6,0x86,0x27,0xBF,0xC1,0x6E,0xDE,0x9A,0x09,0x0D,0xAB,0xE1,0x91,
	    0x56,0xCD,0xB3,0x76,0x0C,0xC3,0xD3,0x9F,0x42,0xB6,0x9B,0xE5,0x23,0xA7,0xAD,0x18,
	    0xC6,0xF4,0xB8,0xBE,0x15,0x43,0x70,0xE0,0xE7,0xBC,0xF1,0xBA,0xA5,0xA6,0x53,0x75,
	    0xE4,0xEB,0xE6,0x85,0x14,0x48,0xDD,0x38,0x2A,0xCC,0x7F,0xB1,0xC0,0x71,0x96,0xF8,
	    0x3F,0x28,0xF2,0x69,0x74,0x68,0xB7,0xA3,0x50,0xD0,0x79,0x1D,0xFC,0xCE,0x8A,0x8D,
	    0x2E,0x62,0x30,0xEA,0xED,0x2B,0x26,0xB9,0x81,0x7C,0x46,0x89,0x73,0xA2,0xF7,0x72
	};
}
