using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameData
{

    private static Dictionary<string, DicEntry> Globals = null;  // refers to Data.Globals

    public static int cash = 0;

    public static void Init()
    {
        Globals = Data.GetGlobals();

    }
    public static void CopyFromGlobalsToGameData()
    {
        // Game specific variables
        cash = Globals["Cash"].i;
    }
    public static void CopyFromGameDataToGlobals()
    {
        // Game specific variables
        Globals["Cash"].i = cash;
    }
}
