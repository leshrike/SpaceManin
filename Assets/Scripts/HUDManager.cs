using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{

    public Text coinsText;
    public static HUDManager sharedInstance;
    public Canvas ingameCanvas;
    private PlayerController controller;
    

    void Awake(){

        if(sharedInstance == null) {

            sharedInstance = this;
        }
    }

    void Start(){

        controller = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update(){

        if(GameManager.sharedInstance.currentGameState == GameState.inGame){
            int coins = GameManager.sharedInstance.collectedObject;
            float score = controller.getTravelledDistance(); 
            coinsText.text = coins.ToString();
        }
    }

    public void ShowHUD(){

        ingameCanvas.enabled = true;
    }

    public void HideHUD(){

        ingameCanvas.enabled = false;
    }
}
