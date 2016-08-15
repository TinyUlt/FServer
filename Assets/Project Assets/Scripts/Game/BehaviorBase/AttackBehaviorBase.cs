using UnityEngine;
using System.Collections;

abstract public class AttackBehaviorBase : MonoBehaviour {

    public enum MODE
    {
        None,
        PlayerMode1,
        PlayerMode2,
        PlayerMode3,
        CrabMode
    }
    public enum CatchType
    {
        God,
        ByHp,
        ByProbability
    }

    public MODE mode;

    public CatchType catchType;

    public int HP;

    private int HPStore;

    public float Probability;

    public int ATK;

    public int coinValue;

    public int destroyDeep;

    bool isDestroy = false;

    private Mission mission;

    [HideInInspector]
    public PoolManager pools;

    [HideInInspector]
    public SpawnerBase spawner;

    [HideInInspector]
    public AttackBehaviorBase injuredAttackBehavior;

    public virtual void Awake()
    {
        if (!GetComponent<MovementData>())
        {
            gameObject.AddComponent<MovementData>();
        }
        injuredAttackBehavior = this;

        HPStore = HP;
    }

    public virtual void OnEnable()
    {
        HP = HPStore;
    }
    public virtual void Start()
    {
        mission = GetComponent<Mission>();

        if (mode != MODE.None)
        {
            var type = mode.ToString();

            setMode(type);
        }

        Invoke("nextFrame", 0);
    }
    public void setMode(string type)
    {
        var mode = gameObject.transform.FindChild("Mode");

        if (mode)
        {
            Destroy(mode.gameObject);
        }
        var modeGameObj = Loader.LoadGameObject("Mode/" + type);

        modeGameObj.name = "Mode";

        modeGameObj.transform.parent = gameObject.transform;

        modeGameObj.transform.position = gameObject.transform.position;

        modeGameObj.transform.rotation = gameObject.transform.rotation;
    }
    public virtual void nextFrame()
    {
    }

    public void destroyWithOnTriggerExit()
    {
        transform.position = Vector3.up * destroyDeep;

        isDestroy = true;
    }
    public void destroyWithOnTriggerExit(float time)
    {
        isDestroy = false;
        CancelInvoke("destroyWithOnTriggerExit");
        Invoke("destroyWithOnTriggerExit", time);
    }
    void FixedUpdate()
    {
        if (isDestroy)
        {
            pools.ReturnObjectToPool(gameObject.name, gameObject);

            isDestroy = false;
        }
    }
    public virtual void catchFish(AttackBehaviorBase catchAttackBehavior, AttackBehaviorBase weaponBehavior)
    {
        if (mission)
        {
            mission.ProcessUseWeaponCatchFish(weaponBehavior.name);
        }
        catchSomething(catchAttackBehavior);
    }
    public virtual void catchSomething(AttackBehaviorBase catchAttackBehavior)
    {
        //玩家捕获到物体
        if(injuredAttackBehavior == this)
        {
            coinValue += catchAttackBehavior.coinValue;

            if (mission)
            {
                mission.ProcessCatchSomething(catchAttackBehavior.gameObject.name);

                mission.ProcessEarnCoin(catchAttackBehavior.coinValue);
            }
        }
        else//子弹捕获到物体
        {
            injuredAttackBehavior.catchSomething(catchAttackBehavior);
        }
        
    }
    public virtual void useWeapon(WeaponBehavior weaponBehavior)
    {
        coinValue -= weaponBehavior.coinValue;

        if (mission)
        {
            mission.ProcessConsumeWeapon(weaponBehavior.name);

            mission.ProcessConsumeCoinValue(weaponBehavior.coinValue);
        }
    }
}
