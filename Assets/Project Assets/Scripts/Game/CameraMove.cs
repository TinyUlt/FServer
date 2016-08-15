using UnityEngine;
using System.Collections;
//test
public class CameraMove : MonoBehaviour {

	public GameObject followObj;

	void Start () {
	
	}
	
	void FixedUpdate() {

        if (followObj)
        {
            transform.position = new Vector3(followObj.transform.position.x, followObj.transform.position.y, transform.position.z);//.x = followObj.transform.position.x;
        }
    }
}
