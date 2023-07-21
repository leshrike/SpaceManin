using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{

    public Text coinsText, scoreText;
    public static GameOverManager sharedInstance;
    public Canvas gameOverCanvas;
    private PlayerController controller;

    void Awake(){

        if(sharedInstance == null){

            sharedInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
            int coins = GameManager.sharedInstance.collectedObject;
            float score = controller.getTravelledDistance(); 
            coinsText.text = coins.ToString();
            scoreText.text = "Score: " + score.ToString("f0"); 
    }

    public void ShowGameOver(){

        gameOverCanvas.enabled = true;
    }

    public void HideGameOver(){
    
        gameOverCanvas.enabled = false;
    }
}
