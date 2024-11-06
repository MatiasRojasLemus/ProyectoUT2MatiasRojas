using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCheckPlayer : MonoBehaviour
{   
    public static bool playerFound = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Player"){
            playerFound = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.tag == "Player"){
            playerFound = false;
        }
    }
}
