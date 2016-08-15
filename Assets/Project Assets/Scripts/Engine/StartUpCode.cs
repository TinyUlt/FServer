using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartUpCode : MonoBehaviour
{
    public TextAsset[] localizationTextAssets;

    public TextAsset globalsText;

    public TextAsset sharedText;

    private bool pInitialized = false;

    private static bool pRanOnce = false;  // prevent multiple instances

    void Awake()
    {
        Debug.Log("[StartUpCode] Awake() called.");

        // Check if this already ran.
        // (PIETER) why do we need this???
        if (pRanOnce)
        {
            Destroy(gameObject);

            return;
        }

        pRanOnce = true;

        DontDestroyOnLoad(gameObject);

        Data.Initialize(globalsText.text, sharedText.text);
    }

    void Update()
    {
        if (!pInitialized)
        {
            // Check if the required scenes are ready to go.
            if (Application.CanStreamedLevelBeLoaded(0) && Application.CanStreamedLevelBeLoaded(1) )
            {
                Initialize();

                pInitialized = true;
            }
        }
    }
    void Initialize()
    {
        Debug.Log("[StartUpCode] Initialize() called.");
        //		Languages.Init(); (DG) Temp disabled here since it's also done in GameData.Start

        // If the game was already initialized, stop now.
        if (pInitialized) return;

        // Overwrite the variables found in the globals with the ones from the savegame.
        if (Data.loadUserData)
        {
            UserData.Load();
            //MissionManager.LoadMissionProgress(GameData.missionProgress);
        }

        // Initializes easy references to some important scripts
        // Scripts.Initialize();

        // This takes the game toward the Loading scene.
        // Then the LoaderScript will take over.
        // This will eventually mtake the game into the Game scene. 
        // This can be either Data.Scene = Menu or Data.Scene = Level 
        // Then, the InterfaceScript will take over.
        //GameData.Start();

        SceneManager.LoadScene("Game");
        //Application.LoadLevel("Game");

    }
}
