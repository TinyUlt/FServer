using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using System;
using System.Reflection;
public class Game : MonoBehaviour {

	//场景列表
	public List<Scene> sceneList;
	//场景横数
	public int a = 1;
	//场景列数
	public int b = 1;
	//路径文件
	public TextAsset globalPath;
	//路径容器
	static public Dictionary<string, List<Vector3>> savedPath = new Dictionary<string, List<Vector3>>() ;
	//路径名字列表
	static public List<string> savedPathName = new List<string> ();

	//场景配置文件
	public TextAsset SceneConfig;

	public int MaxSceneCount;

	public int DelayCreateFishFrame;

	void Start () {
		 
		initPaths ();

		initScenes ();

	}
	
	//定时器刷新
	void FixedUpdate(){
		
		foreach (var scene in sceneList) {

			scene.Refresh ();
		}
	}

	//初始化路径
	void initPaths(){

		string pathString = globalPath.text;

		var onePathDic = Json.Deserialize (pathString) as Dictionary<string, object>;

		foreach (var aa in onePathDic) {

			var bb = aa.Value as List<object>;

			List<Vector3> ff = new List<Vector3> ();

			foreach (var cc in bb) {

				var dd = cc as List<object>;

				var ee = new Vector3 ((float)Convert.ToDouble( dd [0]),(float)Convert.ToDouble( dd [1]),(float)Convert.ToDouble( dd [2]));

				ff.Add (ee);
			}
			savedPath [aa.Key] = ff;

			savedPathName.Add (aa.Key);
		}
	}

	//初始化场景
	void initScenes(){

		readSceneConfig ();

		for (var i = 0; i < a; i++) {

			for (var j = 0; j < b; j++) {
		
				var scene = Scene.MakeNewScene (i*a + j, 960, 640, DelayCreateFishFrame);

				scene.transform.position = new Vector3 (1000 * j, 700 * i, 0);

				sceneList.Add (scene);
			}

		}
	}

	//读取场景配置
	void readSceneConfig(){
	
		string SceneConfigString = SceneConfig.text;

		var SceneConfigDic = Json.Deserialize (SceneConfigString) as Dictionary<string, object>;

		MaxSceneCount = Convert.ToInt32 (SceneConfigDic ["MaxSceneCount"]);

		DelayCreateFishFrame = Convert.ToInt32 (SceneConfigDic ["DelayCreateFishFrame"]);
	}
}
