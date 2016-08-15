using UnityEngine;
using System.Collections;

public class SelectPanel : MonoBehaviour {

    public InterfaceScript interfaceScripte;

    public PlayerBehavior player;

    public void OnButton(GameObject aButton) { OnButton(aButton.name); }

    //public AttackBehaviorBase.MODE mode;

    public void OnButton(string aButtonName)
    {
        Debug.Log("[InterfaceScript] OnButton received: " + aButtonName);

        switch (aButtonName)
        {
            case "SelectBack":
                {
                    interfaceScripte.selectPanel.SetActive(false);

                    interfaceScripte.startPanel.SetActive(true);
                    break;
                }
            case "StartGame":
                {
                    interfaceScripte.selectPanel.SetActive(false);
                    interfaceScripte.gamePanel.SetActive(true);
                    interfaceScripte.game.SetActive(true);

                    //attackBehaviorObject.mode = 
                    break;
                }
            case "PlayerMode1":
                {
                    player.mode = PlayerBehavior.MODE.PlayerMode1;
                    break;
                }
            case "PlayerMode2":
                {
                    player.mode = PlayerBehavior.MODE.PlayerMode2;
                    break;
                }
            case "PlayerMode3":
                {
                    player.mode = PlayerBehavior.MODE.PlayerMode3;
                    break;
                }

        }
    }
}
