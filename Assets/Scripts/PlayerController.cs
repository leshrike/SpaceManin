using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // movement variables
    public float jumpForce = 6f;
    public float runningSpeed = 2f; 
    Rigidbody2D rigidBody;
    public LayerMask groundMask;
    Animator animator;
    Vector3 startPosition;


    public float jumpRaycastDistance = 1.5f;

    private int hp, mp;
    public const int baseHp = 100, baseMp = 15, maxHp = 200, maxMp = 30, minHp = 10, min_mana = 0;

    public const int superJump_Cost = 5;
    public const float SuperJump_Force = 1.8f;

    private const string STATE_ALIVE = "isAlive";
    private const string STATE_ON_THE_GROUND = "isGrounded";

    void Awake(){

        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.position;
        // iniciacion de los valores iniciales de HP y MP
        hp = baseHp;
        mp = baseMp;
    }

    public void StartGame(){
        // resucitacion + caida
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, false );
        // Retrasa en 0.1 segundos la llamada de Restart position
        Invoke("RestartPosition",0.15f);
        
    }

    void RestartPosition(){

        this.transform.position = startPosition;
        this.rigidBody.velocity = Vector2.zero;
        GameObject mainCamera = GameObject.Find("Main Camera");
        mainCamera.GetComponent<CameraFollow>().ResetCameraPosition();
    }

    // Update is called once per frame
    void Update(){
        // bindeo de teclas: espacio y click izquierdo para saltar
        if(Input.GetButtonDown("Jump")){
            Jump(false);
        }

        if(Input.GetKeyDown(KeyCode.M)){
            Jump(true);
        }


        animator.SetBool(STATE_ON_THE_GROUND, IsTouchingGround());
        //Debug.DrawRay(this.transform.position, Vector2.down * 1.5f, Color.red);
    }

    void FixedUpdate(){

      if (GameManager.sharedInstance.currentGameState == GameState.inGame){
            // si la partida ha empezado, establecemos el movimiento automatico
            if(rigidBody.velocity.x < runningSpeed){
                rigidBody.velocity = new Vector2(runningSpeed,rigidBody.velocity.y);
            }
            
        }else{ // si no estamos en la partida
                rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }  
    }

    void Jump(bool isSuper){
        
        float jumpForceFactor = jumpForce;

        if(isSuper && mp>=superJump_Cost){
            mp -= superJump_Cost;
            jumpForceFactor *= SuperJump_Force;
        }
        if (GameManager.sharedInstance.currentGameState == GameState.inGame){
            // prevents player from jumping if the playable character is not grounded
            if (IsTouchingGround()){
                GetComponent<AudioSource>().Play();
                rigidBody.AddForce(Vector2.up * jumpForceFactor, ForceMode2D.Impulse);
            }
        }
    }

    // returns data according to the position of the player. Is he grounded?
    bool IsTouchingGround(){

        if(Physics2D.Raycast(this.transform.position, Vector2.down, jumpRaycastDistance, groundMask)){
            //TODO: code that runs if player touches the ground
            //animator.enabled = true;
            return true;
        }else{
            //TODO: code that runs if player is not touching the ground
            //animator.enabled = false;
            return false;
        }
    }

    public void Die(){

        this.animator.SetBool(STATE_ALIVE, false);
        GameManager.sharedInstance.GameOver();
    }

    public void CollectHP(int points){

        this.hp += points; 

        if(this.hp > maxHp){

            this.hp = maxHp;
        }

        if(this.hp <= 0){
            Die();
            Timer.sharedInstance.stopTimer();
        }
    }

    public void CollectMP(int points){

        this.mp += points; 

        if(this.mp > maxMp){

            this.mp = maxMp;
        }
    }

    public int GetHealth(){
        return hp;
    }

    public int GetMana(){
        return mp;
    }

    public float getTravelledDistance(){
        return this.transform.position.x - startPosition.x;
    }
}
