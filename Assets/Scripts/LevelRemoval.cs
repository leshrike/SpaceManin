using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRemoval : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision){
    
        if (collision.tag == "Player"){
            
            LevelManager.sharedInstance.AddLevelBlock();
            LevelManager.sharedInstance.RemoveLevelBlock();
        }
    }
}
