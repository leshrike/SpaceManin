using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float runningSpeed = 1.5f;
    Rigidbody2D rigidBody;
    public bool facingRight = false;
    private Vector3 startPosition;
    public int enemyDamage = -10;
    
    private void Awake(){

        rigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate(){

        float currentRunningSpeed = runningSpeed;

        if(facingRight){
            currentRunningSpeed = runningSpeed;
            //rotar 180 grados el misil en base a la posicion normal sin alterar su desplazamiento en x
            this.transform.eulerAngles = new Vector3(0,180,0);
        }else{
            currentRunningSpeed = -runningSpeed;
            // volver a la posicion inicial del misil, haciendo que mire a la izda
            this.transform.eulerAngles = Vector3.zero;
        }

        if(GameManager.sharedInstance.currentGameState == GameState.inGame){

            rigidBody.velocity = new Vector2(currentRunningSpeed, rigidBody.velocity.y);
        }
    }  

    private void OnTriggerEnter2D(Collider2D collision){

        if (collision.tag == "Coin"){
            return;
        }

        if (collision.tag == "Player"){
            collision.gameObject.GetComponent<PlayerController>().CollectHP(enemyDamage);
            return;
        }

        // colision con el mundo / escenario
        facingRight = !facingRight;
    }
}
