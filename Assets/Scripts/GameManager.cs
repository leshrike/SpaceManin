using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState{
    menu,
    inGame,
    gameOver
}

public class GameManager : MonoBehaviour
{
    public int collectedObject = 0;
    public GameState currentGameState = GameState.menu;
    public static GameManager sharedInstance;
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
        HUDManager.sharedInstance.HideHUD();
        GameOverManager.sharedInstance.HideGameOver();
    }

    // acciones que se realizan en la inicializacion de juego
    public void startGame(){
        SetGameState(GameState.inGame);
        collectedObject = 0;
    }
    // acciones que se realizan cuando la partida acaba
    public void GameOver(){
        SetGameState(GameState.gameOver);
    }

    // vuelta al menu
    public void backToMenu(){
        SetGameState(GameState.menu);
    }

    private void SetGameState(GameState newGameState){

        if (newGameState == GameState.menu){

            HUDManager.sharedInstance.HideHUD();
            GameOverManager.sharedInstance.HideGameOver();
            MenuManager.sharedInstance.ShowMainMenu();

        }else if(newGameState == GameState.inGame){
           
            LevelManager.sharedInstance.RemoveAllLevelBlocks();
            LevelManager.sharedInstance.GenerateInitialBlocks();
            controller.StartGame();
            MenuManager.sharedInstance.HideMainMenu();
            GameOverManager.sharedInstance.HideGameOver();
            HUDManager.sharedInstance.ShowHUD();
            
           
        }else if(newGameState == GameState.gameOver){
            
            HUDManager.sharedInstance.HideHUD();
            GameOverManager.sharedInstance.ShowGameOver();
            
        }

        this.currentGameState = newGameState;
    }

    public void CollectObject(Collectibles collectable){

        collectedObject += collectable.value;

    }
}
