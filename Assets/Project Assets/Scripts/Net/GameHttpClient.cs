using UnityEngine;
using System.Collections;
using System;
using MiniJSON;
using System.Collections.Generic;
public class GameHttpClient : MonoBehaviour {

	public GameInfo gameInfo;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void NetConnect(Action<bool, string, string, string> callback){

		Debug.Log ("[NetConnect]");

		//测试本机网络
		StartCoroutine (CheckNetConnect((netEnable) => {
			
			Debug.Log ("[NetConnect.CheckNetConnect]="+ netEnable);

			if(netEnable){
				//链接服务器
				StartCoroutine (ServerNetConnect((isConnect, requrestData) => {

					Debug.Log ("[NetConnect.ServerNetConnect, isConnect]="+ isConnect);

					Debug.Log ("[NetConnect.ServerNetConnect, requrestData]="+ requrestData);

					if(isConnect){
						//解析json
						var onePathDic = Json.Deserialize (requrestData) as Dictionary<string, object>;

						var data = onePathDic ["data"] as Dictionary<string, object>;

						var gateway = Convert.ToString (data ["gateway"]);

						var uid = Convert.ToString(data ["uid"]);

						var sessionid = Convert.ToString(data ["sessionid"]);
						//返回给登录脚本
						callback(true, sessionid, uid, gateway);
					}
					else{

						callback(false, "", "", "");
					}
				}));
			}
		}));
	}
	//测试本机网络
	public IEnumerator CheckNetConnect(Action<bool> callback){
		//http get 方法
		WWW www = new WWW (gameInfo.HttpCheck);

		yield return www;
		//检测是否连接成功
		if (!string.IsNullOrEmpty (www.error)) {
			
			callback (false);

		} else {
			
			callback (true);
		}
	}
	//连接服务器网络
	public IEnumerator ServerNetConnect(Action<bool, string> callback){

		WWWForm from = new WWWForm ();

		string cmd = "1030";
		string channel = "2";
		string login_name = "whx";
		string password = "e10adc3949ba59abbe56e057f20f883e";
		string time = secondsSinceEpoch().ToString();
		string sha1 =  "channel="+channel+"%cmd="+cmd+"%login_name="+login_name+"%password="+password+"%t="+time+gameInfo.HttpKey;
		sha1 = SHA1(sha1).ToLower();

		from.AddField ("s", sha1);
		from.AddField ("channel", channel);
		from.AddField ("login_name", login_name);
		from.AddField ("password", password);
		from.AddField ("t", time);
		//http post 方法
		WWW www = new WWW (gameInfo.HttpHost+"?cmd="+cmd,from);

		yield return www;
		//检测是否连接成功
		if (!string.IsNullOrEmpty (www.error)) {

			callback (false, "");

		} else {

			var returnData = System.Text.Encoding.UTF8.GetString (www.bytes);

			callback (true, returnData);
		}
	}
	//sha1签名
	public static string SHA1(string text)
	{
		byte[] cleanBytes = System.Text.Encoding.Default.GetBytes(text);

		byte[] hashedBytes = System.Security.Cryptography.SHA1.Create().ComputeHash(cleanBytes);

		return System.BitConverter.ToString(hashedBytes).Replace("-", "");
	}
	//获取时间
	public static int secondsSinceEpoch(){

		System.TimeSpan t = System.DateTime.UtcNow - new System.DateTime(1970, 1, 1);

		return (int)t.TotalSeconds;
	} 
}
