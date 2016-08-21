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
using Google.Protobuf.Gt;
using Google.Protobuf;
public class MainNet : MonoBehaviour {

	private string key = "#5954d87ccc24fe7527075f2629ec22d26e317af0";

	private string url = "http://175.43.23.15:8080?cmd=1030";


	private string sessionid;

	private string uid;
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

		uid = Convert.ToString(data ["uid"]);

		sessionid = Convert.ToString(data ["sessionid"]);

		print ("gateway:" + gateway);
		var ip_port = gateway.Split (':');
		var ip = ip_port [0];
		var port = ip_port [1];

		//Connect (ip,  Convert.ToInt32( port));
		startNet(ip, Convert.ToInt32( port));
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
	void startNet(string ip, int port){

//		MessageCenter.GetInstance ().ConnectServer (enSocketType.SocketType_Login,ip, port,()=>{
//			StartMainLogon(sessionid, uid);
//
//			Debug.Log("连接服务器成功 hh");
//
//		});
	}
	// Update is called once per frame
	void Update () {
	
	}

	public void StartMainLogon(string Sessionid, string Uid)
	{

		LoginRequest request = new LoginRequest();

		request.Sessionid = sessionid;

		request.Uid = uid;

//		int msgId = 3;
//
//
//		var loads = request.ToByteArray ();
//		codeData (loads);
//
//		byte[] mMagic =System.Text.Encoding.Default.GetBytes ( "GTV1" );//  "GTV1";
//
//		var lenght = mMagic.Length + 2 + 2 + loads.Length;
//
//		var result = new byte[lenght];
//
//		for(var i = 0; i < mMagic.Length; i++){
//			result[i] = mMagic[i];
//		}
//
//		byte[] twochar = new byte[2]	;
//		twochar[0] = (byte)(msgId>>8);
//		twochar[1] = (byte)(msgId>>0);
//
//		for(var i = 0; i < 2; i++){
//			result[i + mMagic.Length] = twochar[i];
//		}
//
//		twochar[0] = (byte)(loads.Length>>8);
//		twochar[1] = (byte)(loads.Length>>0);
//
//		for(var i = 0; i < 2 ; i++){
//			result[i + mMagic.Length + 2] = twochar[i];
//		}
//
//		for(var i =0; i < loads.Length; i++){
//			result[i + mMagic.Length + 2 + 2] = loads[i];
//		}
	

		//SocketServer.Instance.SendMessage (enSocketType.SocketType_Login, request);
	}
}
