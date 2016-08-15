using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// MissionData.
/// <para>Contains all data mission related.</para>
/// </summary>
[System.Serializable]
public class MissionData
{
	public int 		mission 	  = 1;                      // mission number

    public List<string> catchSomething = new List<string>();
    public List<int> catchCount = new List<int>();

    public List<string> consumeWeapon = new List<string>();
    public List<int> consumeWeaponCount = new List<int>();

    public List<string> useWeaponCatchFish = new List<string>();
    public List<int> useWeaponCatchFishCount = new List<int>();

    public Dictionary<string, int> catchs;
    public Dictionary<string, bool> catchsFinish;

    public Dictionary<string, int> consumeWeapons;
    public Dictionary<string, bool> consumeWeaponsFinish;

    public Dictionary<string, int> useWeaponCatchFishs;
    public Dictionary<string, bool> useWeaponCatchFishsFinish;

    public int earnCoin = -1;
    public bool earnCoinFinish;

    public int consumeCoin = -1;
    public bool consumeCoinFinish;

    public void init(int aMission, bool reset = false)
    {
        mission = aMission;

        catchs = new Dictionary<string, int>();
        catchsFinish = new Dictionary<string, bool>();

        consumeWeapons = new Dictionary<string, int>();
        consumeWeaponsFinish = new Dictionary<string, bool>();

        useWeaponCatchFishs = new Dictionary<string, int>();
        useWeaponCatchFishsFinish = new Dictionary<string, bool>();

        if (earnCoin >= 0)
        {
            earnCoin = reset ? 0 : earnCoin;
            earnCoinFinish = false;
        }
        
        if(consumeCoin >= 0)
        {
            consumeCoin = reset ? 0 : consumeCoin;
            consumeCoinFinish = false;
        }

        for (var i = 0; i < catchSomething.Count; i++)
        {
            catchs.Add(catchSomething[i], reset ? 0 : catchCount[i]);
            catchsFinish.Add(catchSomething[i], false);
        }
        for (var i = 0; i < consumeWeapon.Count; i++)
        {
            consumeWeapons.Add(consumeWeapon[i], reset ? 0 : consumeWeaponCount[i]);
            consumeWeaponsFinish.Add(consumeWeapon[i], false);
        }
        for (var i = 0; i < useWeaponCatchFish.Count; i++)
        {
            useWeaponCatchFishs.Add(useWeaponCatchFish[i], reset ? 0 : useWeaponCatchFishCount[i]);
            useWeaponCatchFishsFinish.Add(useWeaponCatchFish[i], false);
        }
    }
}