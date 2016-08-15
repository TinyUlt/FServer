using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Pool manager.
/// Pool stuff for re-use!!
/// 描述：
///     对象池管理
/// 目标：
///     初始化
///     设置根节点
///     维护对象池列表
///     从指定对象池中获取对象
///     回收对象
///     重置管理器
/// </summary>
public  class PoolManager:MonoBehaviour
{
    public enum StaticUnitType
    {
        Coin,
        Mission,
        Crab,

    }
    public enum DynamicUnitType
    {
        EnemyTiny,
        EnemyMid,
        EnemyHug,
        AttackEnemy,
    }
    public enum WEAPONS { Bullet, Fishingnet, BulletWithFishingnet };
     
    //GameObject poolHolder; // Main object that will hold all unique pool holders and their pool objects
	public  Dictionary<string, PoolData> availablePools; // Dictionary of all available pools
	//private  PoolVisualizer poolVisualizer; // Script reference so you can see all pools in Editor (look for the Pools object)
	public  bool debug = false; // Shows debug logs.

    public void Awake()
    {
        availablePools = new Dictionary<string, PoolData>(); // make new dict.

        //poolHolder = gameObject;

        foreach (var item in System.Enum.GetValues(typeof(StaticUnitType)))
        {
            CreatePool("Unit/", item.ToString(), 10);
        }
        foreach (var item in System.Enum.GetValues(typeof(DynamicUnitType)))
        {
            CreatePool("Unit/", item.ToString(), 10);
        }
        foreach (var item in System.Enum.GetValues(typeof(WEAPONS)))
        {
            CreatePool("Unit/", item.ToString(), 1);
        }
    }
	public  void Start()
	{
        //GameObject tPoolHolder = GameObject.Find("Pools"); // Does it already exists? Note: We're looking for 'Effects'. Don't want holder objects for ALL types of effects.
        //if (tPoolHolder == null) poolHolder = new GameObject("Pools"); // No, make a new one.
        //else poolHolder = tPoolHolder; // Yes, store it.
        //poolVisualizer = poolHolder.AddComponent<PoolVisualizer>(); // Add the visualizer
        
	}

	// Create a pool
	public  PoolData CreatePool(string aPath, string aPrefab, int anAmount)
	{
		var poolGameObject = new GameObject(aPrefab + "_Pool"); // New data ref.
        var poolData = poolGameObject.AddComponent<PoolData>();
		poolData.Initialize(aPath, aPrefab, anAmount); // Initialize
        poolGameObject.transform.parent = gameObject.transform;
        //poolData.holder.transform.ResetToParent(poolHolder); // parent this holder to the PoolManager holder
        availablePools[aPrefab] = poolData; // store
		//poolVisualizer.allPoolData.Add(poolData); // add to list so I can see it in Editor
		// return it, so you can do stuff with it when necessary
		return poolData;
	}

	/// <summary>
	/// Does the pool exist.
	/// </summary>
	/// <returns><c>true</c>, if pool exist, <c>false</c> otherwise.</returns>
	/// <param name="aPoolName">A pool name.</param>
	public  bool DoesPoolExist(string aPoolName){
		return availablePools.ContainsKey(aPoolName); // does it exist?
	}

	/// <summary>
	/// Gets an object from pool.
	/// </summary>
	/// <returns>The object from pool.</returns>
	/// <param name="aPoolName">A pool name.</param>
	public  GameObject GetObjectFromPool(string aPoolName){
		if(!availablePools.ContainsKey(aPoolName)){if(debug) Debug.Log("[PoolManager] GetObjectFromPool. This is a last resort fallback! There is no pool with this name: " + aPoolName); return null;}

        var obj = availablePools[aPoolName].GetObject();

        obj.GetComponent<AttackBehaviorBase>().pools = this;
        return obj;
	}

	/// <summary>
	/// Returns an object to its pool.
	/// </summary>
	/// <param name="aPoolName">A pool name.</param>
	/// <param name="anObject">An object.</param>
	public  void ReturnObjectToPool(string aPoolName, GameObject anObject){
		if(!availablePools.ContainsKey(aPoolName)){
			if(debug)Debug.Log("[PoolManager] ReturnObjectToPool. This is a last resort fallback! There is no pool with this name: " + aPoolName + ". This object will be destroyed instead.");
			Object.Destroy(anObject);
		} else availablePools[aPoolName].ReturnObject(anObject);
	}

	/// <summary>
	/// Reset this instance.
	/// </summary>
	public  void Reset(){
		// Clear pools
		List<PoolData> allPoolData = new List<PoolData>();
		allPoolData.AddRange(availablePools.Values);
		PoolData poolData;
		for (int i = allPoolData.Count-1; i >= 0; i--) {
			poolData = allPoolData[i];
			allPoolData.RemoveAt(i);
			poolData.Destroy();
		}

		// new dictionary
		availablePools = new Dictionary<string, PoolData>();
	}
}

