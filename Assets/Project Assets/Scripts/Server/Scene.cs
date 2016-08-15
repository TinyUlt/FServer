using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TypeDefine;
public class Scene :MonoBehaviour {

	//场景id
    public int SceneID;
	//场景宽
    public int Width;
	//场景高
    public int Height;
	//当前帧数
    public int NowFrame;
	//延迟多少帧产生鱼
	public int DelayCreateFishFrame;
	//准备产生鱼的容器
	public Dictionary<int, EstablishFish> ReadyEstablishFishList ;
	//鱼容器
	public  Dictionary<int, Fish> FishList;
	//子弹容器
	public  Dictionary<int, Bullet> BulletList;
	//测试
	public int FishidTemp = 0;
	//绘制边界
	private Vector3[] paths = new Vector3[5];

	//场景一个场景
	public static Scene MakeNewScene( int id ,  int w, int h, int delayCreateFishFrame){

		if (id < 0) {

			return null;
		}
		
		var gameobject = new GameObject ("Scene");

		//背景测试模型
		var modeGameObj = Loader.LoadGameObject("Mode/" + "Background960x640");

		modeGameObj.name = "Background960x640";

		modeGameObj.transform.parent = gameobject.transform;

		modeGameObj.transform.localPosition = new Vector3 (w / 2, h / 2, 0);

		//挂载并初始化场景组建
		var scene = gameobject.AddComponent<Scene>();

		scene.SceneID = id;

		scene.Width = w;

		scene.Height = h;

		scene.NowFrame = 0;

		scene.DelayCreateFishFrame = delayCreateFishFrame;

		scene.ReadyEstablishFishList = new Dictionary<int, EstablishFish> ();

		scene.FishList = new Dictionary<int, Fish>();

		scene.BulletList = new Dictionary<int, Bullet>();

		return scene;
	}

	void Start(){

		InvokeRepeating ("AddFishForTest", 1, 0.2f);

		InvokeRepeating ("AddBulletForTest", 1, 0.2f);
	}

	//添加子弹
    public void AddBullet(Bullet b) {

		this.BulletList.Add (b.Id, b);
    }

	//移除子弹
    public void RemoveBullet(Bullet b){
	
		this.BulletList.Remove (b.Id);

		Destroy (b.gameObject);
    }

	//添加准备鱼
	public void AddReadyFish(EstablishFish fish){
		
		var f = Fish.MakeNewFish (fish.FishId, fish.FishType ,this, fish.PathName);

		this.AddFish (f);
	}

	//添加鱼
    public void AddFish(Fish f){
	
		this.FishList.Add (f.Id, f);
    }

	//移除鱼
    public void RemoveFish(Fish f){
		
		this.FishList.Remove (f.Id);

		Destroy (f.gameObject);
    }

	//测试鱼
	public void AddFishForTest(){

		var fishStruct = new EstablishFish ();

		fishStruct.FishId = ++FishidTemp;

		fishStruct.FishType = 1;

		fishStruct.PathName = Game.savedPathName [Random.Range (0, Game.savedPathName.Count)];

		fishStruct.frame = NowFrame + DelayCreateFishFrame;

		ReadyEstablishFishList[fishStruct.FishId] = fishStruct;
	}

	//测试子弹
	public void AddBulletForTest(){

		var p = new Vector3(Width / 2, 0); // Pos.MakeNewPosForBullet (Width/2, Height/2 ,5, Random.Range(0.0f, 2 * 3.1415f));

		var b = Bullet.MakeNewBullet (1, 5, p,  Random.Range (0.0f, 2 * 3.1415f),this);

		b.Id = ++FishidTemp;

		this.AddBullet (b);
	}

	//定时器
	public void Refresh () {
	    
		this.NowFrame++;


//			if (ishit) {
//				foreach (var fish in FishList.Values) {
//
//					Vector2 fishvc2 = fish.transform.position;
//
//					if (fishvc2.SqrMagnitude (bulletvc2) < fish.NowR () * fish.NowR ()) {
//
//						ishit = true;
//
//						break;
//					}
//				}
//			}

//		}	
//				var fhit = fish.FirstHit ();
//
//				if (fhit != null) {
//					
//					if(fhit.isHit(bullet.NowPos())){
//						
//						var fh = fish.NowHit ();
//
//						bool isehit = false;
//
//						foreach (var h in fh) {
//
//							bs.Add (bullet);
//
//							ishit = true;
//
//							isehit = true;
//						}
//						if(isehit){
//							
//							break;
//						}
//					}
//				}
//			}
//
//			if (ishit) {
//				
//				var bhit = FishHit.MakeNewFishnetHit (bullet);
//
//				foreach (var fish in FishList.Values) {
//					
//					var fhit = fish.FirstHit ();
//
//					if (fhit != null) {
//						
//						if(fhit.isHit(bhit)){
//
//							var fh = fish.NowHit (); 
//
//							bool isehit = false;
//
//							foreach (var h in fh) {
//
//								bhit.isHit (h);
//
//								if (fish.IsDead ()) {
//
//									bullet.KilledFish.Add (fish);
//								}
//								isehit = true;
//							}
//							if(isehit){
//								
//								break;
//							}
//						}
//					}
//				} 
//			}
//		}


//
		//创建准备产生的鱼
		var rfl = new List<EstablishFish>();

		foreach (var fish in ReadyEstablishFishList.Values) {

			if (fish.frame >= NowFrame) {

				AddReadyFish (fish);

				rfl.Add (fish);
			}
		}
		foreach (var fish in rfl) {

			ReadyEstablishFishList.Remove (fish.FishId);
		}

		//遍历鱼
		var fs = new List<Fish>();

		foreach (var fish in FishList.Values) {

			var nowpos = fish.NowPos ();

			if (nowpos != Common.InvalidVec3) {
				//刷新鱼
				fish.Refresh ();

			}  else {

				fs.Add (fish);
			}
		}

		foreach (var fish in fs) {
		
			RemoveFish (fish);
		}

		//遍历子弹 并且检测碰撞
		var bs = new List<Bullet> ();

		foreach (var bullet in BulletList.Values) {

			//刷新子弹
			bullet.Refresh ();

			//碰撞检测
			Vector2 bulletvc2 = bullet.transform.position;

			foreach (var fish in FishList.Values) {

				Vector2 fishvc2 = fish.transform.position;

				if ((bulletvc2 - fishvc2).SqrMagnitude () < (fish.NowR () + bullet.R) * (fish.NowR () + bullet.R)) {

					//ishit = true;
					if (fish.IsDead ()) {

						bullet.KilledFish.Add (fish);


					}

					bs.Add (bullet);

					break;
				}
			}
		}

		//删除子弹和子弹杀死的鱼
		foreach (var bullet in bs) {

			foreach (var fish in bullet.KilledFish) {
				
				this.RemoveFish (fish);
			}
			this.RemoveBullet (bullet);
		}


    }

	//绘制场景边界
	void OnDrawGizmos()
	{
		paths [0] = this.transform.position;

		paths[1] =this.transform.position+ new Vector3(Width,0,0);

		paths[2] = this.transform.position+ new Vector3(Width,Height,0);

		paths [3] =this.transform.position+ new Vector3(0,Height,0);

		paths [4] = this.transform.position;

		iTween.DrawLine(paths, Color.yellow);
	}
}

