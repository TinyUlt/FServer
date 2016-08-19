using UnityEngine;
using System.Collections;
using System.Collections.Generic;



//List<Vector3> pathList = new List<Vector3> ();
public class CreatePath : MonoBehaviour {

	public string pathName;
	public Transform[] paths;
	public int pointCount = 100;
	public List<Vector3> pathListVec3 = new List<Vector3> ();

	void Start()
	{
		OnCreatePath();


	}

	void OnDrawGizmos()
	{
		//在scene视图中绘制出路径与线
		iTween.DrawLine(paths, Color.yellow);

		iTween.DrawPath(paths, Color.red);

	}

	 public void OnCreatePath(){

		Debug.Log ("Create");


		iTween.CreatePath(ref pathListVec3, pointCount, paths);
	}

}
