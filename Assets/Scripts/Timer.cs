using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Timer : MonoBehaviour
{
    float currentTime = 0f;
    int minutes;
    int seconds;
    public Text temporizador;
    public static Timer sharedInstance;
    public float runTime;
    // Start is called before the first frame update
    
    void Awake (){

        if (sharedInstance == null){
            
            sharedInstance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;       
        
        minutes = Mathf.FloorToInt((currentTime/3)/60);
        seconds = Mathf.FloorToInt((currentTime/3)%60);
        
        temporizador.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void stopTimer(){

        runTime = currentTime;
    }
}