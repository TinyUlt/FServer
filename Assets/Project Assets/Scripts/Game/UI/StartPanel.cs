using UnityEngine;
using System.Collections;

public class StartPanel : MonoBehaviour {

    public InterfaceScript interfaceScripte;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnButton(GameObject aButton) { OnButton(aButton.name); }

    public void OnButton(string aButtonName)
    {
        Debug.Log("[InterfaceScript] OnButton received: " + aButtonName);

        switch (aButtonName)
        {
            case "StartGame":
                {
                    interfaceScripte.startPanel.SetActive(false);
                    interfaceScripte.gamePanel.SetActive(true);
                    interfaceScripte.game.SetActive(true);
                    break;
                }
            case "Select":
                {
                    interfaceScripte.startPanel.SetActive(false);
                    interfaceScripte.selectPanel.SetActive(true);
                    break;
                }
            
        }
    }
}
