using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet:MonoBehaviour  {

	public const int MinBulletType = 1;

	public const int MaxBulletType = 10;

	public const int AddFrameForBullet = 0;

	public const int PreCalcFrames = 100;

	public Vector3 InvalidVec3 = new Vector3 (-1000, -1000);
	//子弹id
	public int Id;

	public int Uid;

	public int BulletLv;

	public int BulletType;

	public List<Vector3> PosList;

	public int Speed;

	//public FishHit[] OldHits;

	public int BeginFrame;

	public Scene NowScene;

	public int PreFrame;

	public bool Enable;

	public int FishLockId;

	public int R;

	public int NetR;

	public List<Fish> KilledFish;

	public float deltax;

	public float deltay;

	public static Bullet MakeNewBullet(int bulletType, int speed, Vector3 pos, float angle, Scene s){
	
		if (s == null || speed < 1 || speed > 10000 || bulletType < MinBulletType || bulletType > MaxBulletType ){

			return null;
		}

		var bulletGameObject = new GameObject ("Bullet");

		bulletGameObject.transform.parent = s.transform;

		var modeGameObj = Loader.LoadGameObject("Mode/" + "BulletMode");

		modeGameObj.name = "Mode";

		modeGameObj.transform.parent = bulletGameObject.transform;

		modeGameObj.transform.position = bulletGameObject.transform.position;

		modeGameObj.transform.rotation = bulletGameObject.transform.rotation;

		var bt = bulletGameObject.AddComponent<Bullet> ();

		bt.BulletType = bulletType;

		bt.PosList = new List<Vector3> ();

		bt.PosList.Add (pos);

		bt.Speed = speed;

		bt.NowScene = s;

		bt.BeginFrame = s.NowFrame + AddFrameForBullet;

		bt.PreFrame = 0;

		bt.Enable = true;

		bt.KilledFish = new List<Fish> ();

		bt.R = 5;

		bt.NetR = 30;

		bt.deltax = Mathf.Cos (angle) * speed;

		bt.deltay = Mathf.Sin (angle) * speed;

		if ( bt.PreCalcPosList()){

			bt.transform.position = bt.PosList [0];
			return bt;
		}

		return null;
	}

	public Vector3 NowPos(){

		var index = this.NowScene.NowFrame - this.BeginFrame;

		if (this.PosList.Count <= index) {
			this.PreCalcPosList ();
		} 
		if(this.PosList.Count <= index){
			return InvalidVec3;	
		}

		return this.PosList[index];
	}
	public Vector3 NextPos(Vector3 pos){

		pos.x += deltax;

		pos.y += deltay;

		var scene = this.NowScene;
		if (pos.x >= scene.Width) {

			pos.x = scene.Width - (pos.x - scene.Width);

			deltax = -deltax;

		} else if (pos.x <= 0) {

			pos.x = 0 - pos.x;

			deltax = -deltax;
		}

		if (pos.y >= scene.Height) {

			pos.y = scene.Height - (pos.y - scene.Height);

			deltay = -deltay;

		} else if (pos.y <= 0) {

			pos.y = 0 - pos.y;

			deltay = -deltay;
		}

		return pos;

	}
	public bool PreCalcPosList(){

		if(this.PreFrame + this.BeginFrame - this.NowScene.NowFrame >= PreCalcFrames){
			return true;
		}

		var nowPos = this.PosList [this.PosList.Count - 1];

		for (var i = 0; i < PreCalcFrames; i++) {
		
			nowPos = NextPos (nowPos);//nowPos.NextPosOfBounce (this.Speed, this.NowScene);

			this.PosList.Add (nowPos);

			this.PreFrame++;
				
		}

		return true;
	}

	public void Refresh(){
		
		var nowpos = this.NowPos ();

		if (nowpos != InvalidVec3) {

			transform.localPosition = new Vector3 (nowpos.x , nowpos.y , 0);
		}
	}
}

