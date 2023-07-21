using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D (Collider2D collision){

        // si el elemento que colisiona tiene la tag de player se ejecuta el codigo
        if (collision.tag == "Player"){

            PlayerController controller = 
                collision.GetComponent<PlayerController>();
            
            controller.Die();
        }
    } 
}
