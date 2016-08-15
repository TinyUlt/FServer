using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InterfaceScript : MonoBehaviour {

    public GameObject startPanel;

    public GameObject gamePanel;

    public GameObject selectPanel;

    public GameObject game;

    void Awake()
    {
        if (GameObject.Find("StartUp") == null)
        {
            Debug.LogWarning("[LoaderScript] Switching to StartUp scene!");

            gameObject.SetActive(false);

            SceneManager.LoadScene("StartUp");

            return;
        }

        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(false);
        }

        game.SetActive(false);

        startPanel.SetActive(true);

        gameObject.transform.FindChild("FPS").gameObject.SetActive(true);
    }
    void Start()
    {
        StartGame();
    }
    void StartGame()
    {

    }
}
