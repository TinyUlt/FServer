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


public class MainNet : MonoBehaviour {

	private string key = "#5954d87ccc24fe7527075f2629ec22d26e317af0";

	private string url = "http://175.43.23.15:8080?cmd=1030";

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

		var uid = data ["uid"];

		var sessionid = data ["sessionid"];

		print ("gateway:" + gateway);
		var ip_port = gateway.Split (':');
		var ip = ip_port [0];
		var port = ip_port [1];

		//Connect (ip,  Convert.ToInt32( port));
		startNet(ip, Convert.ToInt32( port));
	}


	void startNet(string ip, int port){
	
//		var name = "test123123";
//		var password = "123123";
		MessageCenter.GetInstance ().ConnectServer (enSocketType.SocketType_Login,ip, port,()=>{
			//StartMainLogon(name, password);

			Debug.Log("连接服务器成功 hh");
		});
	}
	// Update is called once per frame
	void Update () {
	
	}

	public void StartMainLogon(string accounts, string password)
	{
		CMD_GP_LogonAccounts _logonStruct = new CMD_GP_LogonAccounts();

		_logonStruct.dwPlazaVersion = GameUtils.GetInstance().GetPlazaVersion();
		_logonStruct.szMachineID = GameUtils.GetInstance().GetMachineID();
		_logonStruct.dwClientIP = GameUtils.GetInstance().GetIP();
		_logonStruct.szPassword = GameUtils.GetInstance().GetMD5String(password);
		_logonStruct.szAccounts = accounts.ToCharArray();
		_logonStruct.cbValidateFlags = GameUtils.GetInstance().GetValidateFlags();
		_logonStruct.szPhonePassword = "".ToCharArray();
		_logonStruct.dwLogonType = GameUtils.GetInstance().GetLogonType();
		_logonStruct.dwClientVersion = 5003;
		_logonStruct.szCdkey = "264bfbefa3525dfc5107f6f2d1e68669".ToCharArray();

		//UserData.GetInstance ().userPassword = password;

		SocketServer.Instance.SendMessage (_logonStruct,enSocketType.SocketType_Login);
	}
}
