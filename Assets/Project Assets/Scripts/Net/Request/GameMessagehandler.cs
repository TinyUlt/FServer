using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Google.Protobuf.Gt;
using Google.Protobuf;
using System;
public static class GameMessagehandler {

	//发送
	public static Dictionary<Type, int> RequsetIdsByType;

	public static Dictionary<string, int> RequsetIdsByName;
	//接受
	public static Dictionary<int, Type> MsgMaps;

	public static void init(){

		RequsetIdsByType = new Dictionary<Type, int> ();

		RequsetIdsByName = new Dictionary<string, int> ();

		MsgMaps = new Dictionary<int, Type> ();

		//消息的定义
		setReflect (1,  typeof(HHRequest));

		setReflect (2,  typeof(HHResponse));

		setReflect (3,  typeof(LoginRequest));

		setReflect (4,  typeof(LoginResponse));
	}

	public static void setReflect(int id, Type type){

		RequsetIdsByType.Add (type, id);

		RequsetIdsByName.Add (type.Name, id);

		MsgMaps.Add (id, type);
	}

	public static IMessage DeserializePacket(byte[] data, int id){

		var type = MsgMaps [id];

		IMessage msg = System.Activator.CreateInstance (type) as IMessage;

		msg.MergeFrom (data);

		return msg;
	}

	//消息分发
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
