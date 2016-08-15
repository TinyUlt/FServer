using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fish :MonoBehaviour {

	public const int AddFrameForFish = 0;

	public int Id;

	public int FishType;

	public List<Vector3> PosList;

	//public List<Vector3> VecList;

	//public List<FishHit> OldHits;

	public int BeginFrame;

	public Scene NowScene;

	public bool Enable;

	public int R;

	public string Pathid;


	public static Fish MakeNewFish(int fishid, int fishtype, Scene s, string pathid){

		if (s == null  ) {
			return null;
		}

		var fishGameObject = new GameObject ("Fish");

		fishGameObject.transform.parent = s.transform;



		var modeGameObj = Loader.LoadGameObject("Mode/" + "PlayerMode4");

		modeGameObj.name = "Mode";

		modeGameObj.transform.parent = fishGameObject.transform;

		modeGameObj.transform.position = fishGameObject.transform.position;

		modeGameObj.transform.rotation = fishGameObject.transform.rotation;

		var tf = fishGameObject.AddComponent<Fish> ();

		tf.Id = fishid;

		tf.FishType = fishtype;

		tf.PosList = new List<Vector3>();

		tf.BeginFrame = s.NowFrame + AddFrameForFish;

		tf.NowScene = s;

		tf.Enable = true;

		tf.R = 18;
		//tf.OldHits = new List<FishHit> ();
		tf.Pathid = pathid;

		if (  tf.PreCalcPosList () /*&& tf.InitOldHits()*/) {
			 
			tf.transform.position = tf.PosList [0];

			return tf;
		}
	
		return null;
	}
		

//	public List<FishHit> NowHit(){
//
//		var hs = new List<FishHit> ();
//
//		var nowpos = this.NowPos ();
//
//		if (nowpos == null) {
//			return hs;
//		}
//
//		var cost = Mathf.Cos (nowpos.Angle);
//		var sint = Mathf.Sin (nowpos.Angle);
//
//		foreach (var p in this.OldHits) {
//			hs.Add (p.NewOXY ((float)nowpos.X, (float)nowpos.Y, cost, sint));
//		}
//		return hs;
//	}

//	public FishHit FirstHit(){
//		var nowpos = this.NowPos ();
//
//
//
//		if (nowpos != InvalidVec3 && this.OldHits.Count > 0) {
//
//			return FishHit.MakeNewFishHit ((int)nowpos.x, (int)nowpos.y, this.R);
//
//		} else {
//			return null;
//		}
//	}
	public void Refresh(){

		var nowpos = this.NowPos ();

		if (nowpos != Common.InvalidVec3) {

			transform.localPosition = nowpos;//new Vector3 (nowpos.x + NowScene.PositionOffsetX, nowpos.Y + NowScene.PositionOffsetY, nowpos.Angle);

			transform.localScale =Vector3.one *  NowScale ();

			var pos = NextPos();

			

			if (pos != Common.InvalidVec3) {


				transform.LookAt (pos + NowScene.transform.position);//(new Vector3 (pos.X + NowScene.PositionOffsetX, pos.Y + NowScene.PositionOffsetY, pos.Angle));
			}

		}

	}
	public int NowR(){
	
		return (int)(NowScale () * (float)this.R);
	}

	public float NowScale(){

		var index = this.NowScene.NowFrame - this.BeginFrame;

		if (this.PosList.Count <= index) {

			return 1.0f;

		} else {

			var s = this.PosList [index].z / 500.0f;
			return 1-s;
		}
	}
	public Vector3 NowPos(){
		
		var index = this.NowScene.NowFrame - this.BeginFrame;

		return GetPos (index );
		
	}
	public Vector3 NextPos(){
		
		var index = this.NowScene.NowFrame - this.BeginFrame;

		return GetPos (index + 1);
	}
	public Vector3 GetPos(int index){

		if (this.PosList.Count <= index) {

			return Common.InvalidVec3;

		} else {

			return this.PosList [index];
		}
	}
//	public Vector3 NowVec(){
//		
//		var index = this.NowScene.NowFrame - this.BeginFrame;
//
//		if (this.PosList.Count <= index) {
//
//			return null;
//
//		} else {
//
//			return this.VecList [index];
//		}
//	}

	public bool PreCalcPosList(){
	
//		var beginx = this.PosList [0].X;
//
//		var beginy = this.PosList [0].Y;
//
//		var beginagle = this.PosList [0].Angle;
//
//		for (var i = 0; i < 1000; i++) {
//			
//			PosList.Add(Pos.MakeNewPos(i,(int)(i*0.5f),0));
//		}	
		PosList = Game.savedPath[Pathid];
		//PosList = CreatePath.pathListVec3;
		//VecList = Test.pathListVec3;



		return true;
	}

	public bool InitOldHits(){

//		var h = FishHit.MakeNewFishHit ((int)this.PosList [0].x, (int)this.PosList [0].Y, 10);
//		var h1 = FishHit.MakeNewFishHit ((int)this.PosList [0].X - 20, (int)this.PosList [0].Y, 10);
//		var h2 = FishHit.MakeNewFishHit ((int)this.PosList [0].X + 20, (int)this.PosList [0].Y, 10);
//
//		this.OldHits.Add (h);
//		this.OldHits.Add (h1);
//		this.OldHits.Add (h2);

		//this.R = 25;
		return true;
	}

	public bool IsDead(){

		return Random.Range(0,100)>90;
	}
}

