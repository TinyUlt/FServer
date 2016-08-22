using UnityEngine;
using System.Collections;
using Google.Protobuf.Gt;
using Google.Protobuf;
public class HeartBeat : MonoBehaviour {
	
	public GameTcpClient tcpClient;

	public float interval;
	// Use this for initialization
	void Start () {

		InvokeRepeating ("beat", 0, interval);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void beat(){


		if (tcpClient != null && tcpClient.m_isConnect) {

			HHRequest hhr = new HHRequest ();

			tcpClient.SendPacket (hhr);

		}
	}
}
