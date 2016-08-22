using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Google.Protobuf.Gt;
using Google.Protobuf;
using System;
public static class GameMessagehandler {

	//发送
	public static Dictionary<string, int> RequsetIds;
	//接受
	public static Dictionary<int, string> MsgMaps;

	public static void init(){

		RequsetIds = new Dictionary<string, int> ();

		MsgMaps = new Dictionary<int, string> ();

		setReflect (3,  "LoginRequest");

		setReflect (4,  "LoginResponse");
	}

	public static void setReflect(int id, string type){

		RequsetIds.Add (type, id);

		MsgMaps.Add (id, type);
	}

	public static IMessage DeserializePacket(byte[] data, int id){

		IMessage msg = null;

		var name = MsgMaps [id];

		switch (name) {

		case "LoginResponse":
			{
				msg = LoginResponse.Parser.ParseFrom(data);
				break;
			}
		default:
			{
				break;
			}
		}

		return msg;
	}

	public static void MessageDispatch(IMessage msg){

		var name = msg.Descriptor.Name;

		switch (name) {

		case "LoginResponse":
			{
				LoginResponse lr = msg as LoginResponse;

				Debug.Log (lr);

				break;
			}

		default:
			{
				break;
			}
		}
	}
}
