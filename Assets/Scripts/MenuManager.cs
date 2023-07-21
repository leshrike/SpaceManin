using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // singleton de menu manager
    public static MenuManager sharedInstance;
    public Canvas menuCanvas;

    void Awake(){

        if(sharedInstance == null){
            sharedInstance = this;
        }
    }

    public void ShowMainMenu(){

        menuCanvas.enabled = true;
    }

    public void HideMainMenu(){

        menuCanvas.enabled = false;
    }

    public void ExitGame(){
        
        // bloque de codigo en funcion de la plataforma de desarrollo
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;        
        #else
            Application.Quit(); 
        #endif
    }
}
