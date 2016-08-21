using UnityEngine;
using System;
using System.Collections;
using System.Security;
using System.Security.Cryptography;
using MiniJSON;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class HttpRq : MonoBehaviour {

	private string key = "#5954d87ccc24fe7527075f2629ec22d26e317af0";

	private string url = "http://175.43.23.15:8080";

	// Use this for initialization
	IEnumerator Start ()
	{
		WWWForm from = new WWWForm ();

		string cmd = "1030";
		string channel = "2";
		string login_name = "whx";
		string password = "e10adc3949ba59abbe56e057f20f883e";
		string time = secondsSinceEpoch().ToString();


		string sha1 =  "channel="+channel+"%cmd="+cmd+"%login_name="+login_name+"%password="+password+"%t="+time+key;
		sha1 = SHA1(sha1).ToLower();

		from.AddField ("s", sha1);
		from.AddField ("channel", channel);
		from.AddField ("login_name", login_name);
		from.AddField ("password", password);
		from.AddField ("t", time);

		WWW www = new WWW (url,from);

		yield return www;

		var returnData = System.Text.Encoding.UTF8.GetString (www.bytes);

		print (returnData);

		var onePathDic = Json.Deserialize (returnData) as Dictionary<string, object>;

		var data = onePathDic ["data"] as Dictionary<string, object>;

		var gateway = Convert.ToString (data ["gateway"]);

		var uid = data ["uid"];

		var sessionid = data ["sessionid"];

		print ("gateway:" + gateway);
		var ip_port = gateway.Split (':');
		var ip = ip_port [0];
		var port = ip_port [1];

		Connect (ip,  Convert.ToInt32( port));
	}



	public static string SHA1(string text)
	{
		byte[] cleanBytes = System.Text.Encoding.Default.GetBytes(text);
		byte[] hashedBytes = System.Security.Cryptography.SHA1.Create().ComputeHash(cleanBytes);
		return System.BitConverter.ToString(hashedBytes).Replace("-", "");
	}

	public static int secondsSinceEpoch(){
		
		System.TimeSpan t = System.DateTime.UtcNow - new System.DateTime(1970, 1, 1);

		return (int)t.TotalSeconds;
	} 

	enum SocketState{
		None,           //无
		Connecting,     //链接中
		ConnectSuccess, //链接成功
		ConnectFail,    //链接失败
		ConnectEnd,     //链接完成
		SocketError,    //出现错误
		CloseConnect,   //关闭链接
	}

	string m_remoteIP;
	int m_portNum;
	private Socket m_socket;
	private SocketState m_socketState;
	private bool m_isConnect = false;
	public bool Connect(string remoteIP,int portNum){
		


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

	public void Close(){
		m_isConnect = false;
		//		m_socket.Shutdown (SocketShutdown.Both);
		m_socket.Close();
	}
	private void InitNetLoopThread(){
		Debug.Log ("连接服务器成功");
//		m_SendPackList = new LinkedList<NetPacket> ();
//		m_sendObject = new object ();
//		m_sendEvent = new ManualResetEvent (false);
//
//		m_ReceivePackList = new LinkedList<NetPacket> ();
//		m_receiveObject = new object ();
//
//		Thread receiveThread = new Thread (ReceiveLoopThread);
//		receiveThread.Start ();
//		Thread sendThread = new Thread (SendLoopThread);
//		sendThread.Start ();

	}

}
