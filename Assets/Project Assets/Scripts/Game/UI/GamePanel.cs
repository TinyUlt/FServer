using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GamePanel : MonoBehaviour {

    public GameObject showAndControlGameObject;

    public Text cashValueText;

    public Text MissionText;

    public Text TargetMissionText;

    public XTweenScale coinValueAnimation;

    [HideInInspector]
    public Shoot shootObject;

    [HideInInspector]
    public PlayerBehavior attackBehaviorObject;

    [HideInInspector]
    public Mission mission;

    void Awake()
    {

    }

    void Start()
    {
        shootObject = showAndControlGameObject.GetComponent<Shoot>();

        attackBehaviorObject = showAndControlGameObject.GetComponent<PlayerBehavior>();

        mission = showAndControlGameObject.GetComponent<Mission>();

        cashValueText.text = attackBehaviorObject.coinValue.ToString();
    }

    void Update () {

        var money = "$" + GenericFunctionsScript.AddSeparatorInInt(attackBehaviorObject.coinValue, ",");

        if(cashValueText.text != money)
        {
            cashValueText.text = money;

            coinValueAnimation.SetActive(true);
        }

        MissionText.text = mission.currentMissionString ;

        TargetMissionText.text = mission.targetMissionString; 
    }

   
    public void OnButton(GameObject aButton) { OnButton(aButton.name); }

    public void OnButton(string aButtonName)
    {
        Debug.Log("[InterfaceScript] OnButton received: " + aButtonName);

        switch (aButtonName)
        {
            case "Bullet":
                {
                    shootObject.setWeapon(aButtonName);
                    break;
                }
            case "Fishingnet":
                {
                    shootObject.setWeapon(aButtonName);
                    break;
                }
            case "BulletWithFishingnet":
                {
                    shootObject.setWeapon(aButtonName);
                    break;
                }
            case "PlayerMode1":
                {
                    attackBehaviorObject.setMode(aButtonName);
                    break;
                }
            case "PlayerMode2":
                {
                    attackBehaviorObject.setMode(aButtonName);
                    break;
                }
            case "PlayerMode3":
                {
                    attackBehaviorObject.setMode(aButtonName);
                    break;
                }
        }
    }
}
