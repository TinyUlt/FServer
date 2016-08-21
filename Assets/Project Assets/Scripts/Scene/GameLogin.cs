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

		httpClient.NetConnect ((isConnectHttp, sessionid, uid, gateway)=>{
			
			Debug.Log ("[TryLogin NetConnect]="+isConnectHttp);

			if(isConnectHttp){

				var ip_port = gateway.Split (':');

				var ip = ip_port [0];

				var port = ip_port [1];

				tcpClient.init(ip, port, (isConnectTcp)=>{

					Debug.Log ("[TryLogin TCPconnect]="+isConnectTcp);

					if(isConnectTcp){

						GameMessagehandler.init();

						LoginRequest request = new LoginRequest();

						request.Sessionid = sessionid;

						request.Uid = uid;

						tcpClient.SendPacket(request);
					}
				});
			}
		});
	}
}
