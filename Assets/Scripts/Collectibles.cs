using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectiblesType{

    healthPotion,
    manaPotion,
    coin
}
public class Collectibles : MonoBehaviour
{

    public CollectiblesType type = CollectiblesType.coin;
    private SpriteRenderer sprite;
    private CircleCollider2D itemCollider;
    bool hasBeenCollected = false;
    public int value = 1;
    GameObject player;

    private void Start(){

        player = GameObject.Find("Player");
    }

    private void Awake(){

        sprite = GetComponent<SpriteRenderer>();
        itemCollider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision){

        if(collision.tag == "Player"){

            PickUp();
        }
    }

    private void Show(){

        sprite.enabled = true;
        itemCollider.enabled = true;
    }

    private void Hide(){

        sprite.enabled = false;
        itemCollider.enabled = false;
        hasBeenCollected = true;
    }

    private void PickUp(){

        hasBeenCollected = false;
        Hide();

        switch(this.type)
        {

            case CollectiblesType.coin:
                // sound is played
                // ui is updated
                GameManager.sharedInstance.CollectObject(this);
                GetComponent<AudioSource>().Play();
                break;
            
            case CollectiblesType.manaPotion:
               
                player.GetComponent<PlayerController>().CollectMP(this.value);
                GetComponent<AudioSource>().Play();
                // sound is played
                // ui is updated
                break;
            
            case CollectiblesType.healthPotion:
               
                player.GetComponent<PlayerController>().CollectHP(this.value);
                GetComponent<AudioSource>().Play();
                // sound is played
                // ui is updated
                break;
        }
    }
}
