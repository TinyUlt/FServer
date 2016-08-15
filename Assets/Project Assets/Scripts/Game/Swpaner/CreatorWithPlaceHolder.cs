using UnityEngine;
using System.Collections;

public class CreatorWithPlaceHolder : SpawnerBase {

    public GameObject PlaceHolder;

	void Start () {

        foreach (Transform item in PlaceHolder.transform)
        {
            var name = item.name;

            var child = pools.GetObjectFromPool(name);

            child.GetComponent<AttackBehaviorBase>().spawner = this;

            child.transform.parent = gameObject.transform;

            child.transform.position = item.transform.position;
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
