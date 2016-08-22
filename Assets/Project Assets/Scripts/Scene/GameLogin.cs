using UnityEngine;
using System.Collections;
using Google.Protobuf.Gt;
using Google.Protobuf;
public class GameLogin : MonoBehaviour {

	public GameHttpClient httpClient;

	public GameTcpClient tcpClient;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TryLogin(){

		Debug.Log ("[TryLogin]");

		httpClient.NetConnect ((isConnectNet, errorCode, sessionid, uid, gateway)=>{
			
			if(isConnectNet){

				Debug.Log("网络活跃");	

				if(errorCode == 0){

					//var a = ;
					Debug.Log(string.Format("http 服务器请求成功，获取数据. gateway:{0}, sessionid:{1}, uid:{2}", gateway, sessionid, uid));

					var ip_port = gateway.Split (':');

					var ip = ip_port [0];

					var port = ip_port [1];

					bool isCreated = tcpClient.init(ip, port, (isConnectTcp)=>{

						if(isConnectTcp){

							Debug.Log("tcp 服务器连接成功");	

							GameMessagehandler.init();

							LoginRequest request = new LoginRequest();

							request.Sessionid = sessionid;

							request.Uid = uid;

							tcpClient.SendPacket(request);
						
						}else{

							Debug.Log("tcp 服务器连接失败!!!");	
						}
					});
					if(isCreated){

						Debug.Log("创建网络成功");
					}else{
						
						Debug.Log("创建网络失败!!!");
					}
				}else{

					Debug.Log("登录错误!!!:"+errorCode);
				}

			}else{
				
				Debug.Log("网络不活跃，请检测网络!!!");				
			}
		});
	}
}
