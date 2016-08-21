using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public static class GameMessagehandler {

	//发送
	public static Dictionary<string, int> RequsetIds;
	//接受
	public static Dictionary<int, string> MsgMaps;

	public static void init(){

		RequsetIds = new Dictionary<string, int> ();

		MsgMaps = new Dictionary<int, string> ();

		setReflect (3, "LoginRequest");
	}

	public static void setReflect(int id, string name){

		RequsetIds.Add (name, id);

		MsgMaps.Add (id, name);
	}

}
