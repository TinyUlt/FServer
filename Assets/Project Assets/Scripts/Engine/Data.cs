using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Data {

    private static Dictionary<string, DicEntry> Globals = null;

    public static Dictionary<string, DicEntry> Shared = null;

    public static string versionNumber = "";

    public static bool loadUserData = false;

    public static float musicVolume = 1.0f;

    public static List<string> saveList = null;

    public static void Initialize(string aGlobalsText, string aSharedText)
    {
        Globals = new Dictionary<string, DicEntry>();

        Shared = new Dictionary<string, DicEntry>();

        TextLoader.LoadText(aGlobalsText, Globals);

        TextLoader.LoadText(aSharedText, Shared);

        GameData.Init();

        CopyFromGlobalsToData();
    }

    public static void CopyFromGlobalsToData()
    {
        versionNumber = Globals["VersionNumber"].s;

        loadUserData = Globals["LoadUserData"].b;

        musicVolume = Globals["MusicVolume"].f;

        saveList = new List<string>();

        foreach (DicEntry tDicEntry in Globals["SaveList"].l)
            saveList.Add(tDicEntry.s);

        GameData.CopyFromGlobalsToGameData();
    }

    // Copy entries from Data to Globals dictionary (yes, a bit annoying..., do not have a better solution for now...)
    // This is so all data can be accessed from the Data class instead of through Data.Globals  (which was fine for me, but understandably confusing for others)
    public static void CopyFromDataToGlobals()
    {
        Globals["VersionNumber"].s = versionNumber;

        Globals["LoadUserData"].b = loadUserData;

        Globals["MusicVolume"].f = musicVolume;

        Globals["SaveList"].l.Clear();

        foreach (string tStr in saveList)
            Globals["SaveList"].l.Add(new DicEntry(tStr));

        GameData.CopyFromGameDataToGlobals();
    }

    public static Dictionary<string, DicEntry> GetGlobals()
    {
        return Globals;
    }
}
