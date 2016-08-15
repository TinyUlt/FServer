using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using MiniJSON;
public class StorePath : MonoBehaviour {

	// Use this for initialization
	public string fileName;
	void Start () {


		string path=Application.dataPath + "/Project Assets/Scripts/Server";

		//DeleteFile (path, fileName + ".json");

		Dictionary<string, object> savedPath = new Dictionary<string, object>();

		var children = GetComponentsInChildren<CreatePath> ();

		foreach(var createPath in children){

			createPath.OnCreatePath ();

			List<List<float>> pathStore = new List<List<float>> ();



			foreach (var vec3 in createPath.pathListVec3) {

				List<float> pointStore = new List<float> ();
				pointStore.Add (vec3.x);
				pointStore.Add (vec3.y);
				pointStore.Add (vec3.z);
				pathStore.Add (pointStore);
			}




			savedPath [createPath.name] = pathStore;



		}

		string dataAsString = Json.Serialize(savedPath);

		//Debug.Log (dataAsString);


		createORwriteConfigFile (path, fileName + ".json", dataAsString);


	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void createORwriteConfigFile(string path,string name,string info)
	{
		StreamWriter sw;          
		FileInfo t = new FileInfo(path+"//"+ name);          
		//		if(!t.Exists)          
		//		{            
					sw = t.CreateText();
		//		}          
		//		else      
		//		{
		//			sw = t.AppendText();         
		//		} 
		//sw = t.CreateText();
		sw.WriteLine(info);
		sw.Close();
		sw.Dispose();
	}
	void DeleteFile(string path,string name)
	{
		File.Delete(path+"//"+ name); 
	}  
}
