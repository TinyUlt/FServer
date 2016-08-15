using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Mission : MonoBehaviour
{
    private MissionData targetMission; 

    private MissionData currentMission;

    string earnCoinFormat = "earnCoin X {0} {1}\n";

    string consumeCoinFormat = "consumeCoin X {0} {1}\n";

    string catchSomethingFormat = "catch {0} X {1} {2}\n";

    string consumeWeaponFormat = "consume {0} X {1} {2}\n";

    string useWeaponCatchFishFormat = "use {0} catch fishs X {1} {2}\n";

    [HideInInspector]
    public string currentMissionString = "";

    [HideInInspector]
    public string targetMissionString = "";

    void Start()
    {
        SetupMission(1);
    }

    public void SetupMission(int aMission)
    {
        targetMission = new MissionData();

        targetMission = GenericFunctionsScript.ParseDictionaryToClass(Data.Shared["Missions"].d["Mission" + aMission].d, targetMission) as MissionData;

        targetMission.init(aMission);

        currentMission = new MissionData();

        currentMission = GenericFunctionsScript.ParseDictionaryToClass(Data.Shared["Missions"].d["Mission" + aMission].d, currentMission) as MissionData;

        currentMission.init(aMission, true);

        missionToString(ref targetMission, ref targetMissionString);

        missionToString(ref currentMission, ref currentMissionString);
    }
    public void ProcessCatchSomething(string aTarget)
    {
        if (currentMission.catchs.ContainsKey(aTarget))
        {
            currentMission.catchs[aTarget] += 1;

            if (currentMission.catchs[aTarget] == targetMission.catchs[aTarget])
            {
                currentMission.catchsFinish[aTarget] = true;
            }
            missionToString(ref currentMission, ref currentMissionString);
        }
    }
    public void ProcessEarnCoin(int aCoinValue)
    {
        if (currentMission.earnCoin >= 0)
        {
            currentMission.earnCoin += aCoinValue;

            if (currentMission.earnCoin == targetMission.earnCoin)
            {
                currentMission.earnCoinFinish = true;
            }
            missionToString(ref currentMission, ref currentMissionString);
        }
    }
    public void ProcessConsumeWeapon(string aWeapon)
    {
        if (currentMission.consumeWeapons.ContainsKey(aWeapon))
        {
            currentMission.consumeWeapons[aWeapon] += 1;

            if (currentMission.consumeWeapons[aWeapon] == targetMission.consumeWeapons[aWeapon])
            {
                currentMission.consumeWeaponsFinish[aWeapon] = true;
            }
            missionToString(ref currentMission, ref currentMissionString);
        }
    }
    public void ProcessConsumeCoinValue(int coinValue)
    {
        if (currentMission.consumeCoin >= 0)
        {
            currentMission.consumeCoin += coinValue;

            if (currentMission.consumeCoin == targetMission.consumeCoin)
            {
                currentMission.consumeCoinFinish = true;
            }
            missionToString(ref currentMission, ref currentMissionString);
        }
    }

    public void ProcessUseWeaponCatchFish(string weaponType)
    {
        if (targetMission.useWeaponCatchFishs.ContainsKey(weaponType))
        {
            currentMission.useWeaponCatchFishs[weaponType] += 1;

            if (currentMission.useWeaponCatchFishs[weaponType] == targetMission.useWeaponCatchFishs[weaponType])
            {
                currentMission.useWeaponCatchFishsFinish[weaponType] = true;
            }
            missionToString(ref currentMission, ref currentMissionString);
        }
    }
    public void missionToString(ref MissionData missionData, ref string missionString)
    {
        missionString = "";

        if (missionData.earnCoin >= 0)
        {
            missionString += string.Format(earnCoinFormat, missionData.earnCoin,missionData.earnCoinFinish?"Done":"");
        }
        if (missionData.consumeCoin >= 0)
        {
            missionString += string.Format(consumeCoinFormat, missionData.consumeCoin, missionData.consumeCoinFinish?"Done":"");
        }
        foreach (var item in missionData.catchs)
        {
            if (missionData.catchs.ContainsKey(item.Key))
            {
                missionString += string.Format(catchSomethingFormat, item.Key, item.Value,missionData.catchsFinish[item.Key]?"Done":"");
            }
        }
        foreach (var item in missionData.consumeWeapons)
        {
            if (missionData.consumeWeapons.ContainsKey(item.Key))
            {
                missionString += string.Format(consumeWeaponFormat, item.Key, item.Value,missionData.consumeWeaponsFinish[item.Key]?"Done":"");// item.Key + " X " + item.Value + "\n";

            }
        }
        foreach (var item in missionData.useWeaponCatchFishs)
        {
            if (missionData.useWeaponCatchFishs.ContainsKey(item.Key))
            {
                missionString += string.Format(useWeaponCatchFishFormat, item.Key, item.Value,missionData.useWeaponCatchFishsFinish[item.Key]?"Done":"");//item.Key + " X " + item.Value + "\n";
            }
        }
    }
}
