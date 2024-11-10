using UnityEngine;

public class EnemyCheckPlayer : MonoBehaviour
{   
    public static bool playerFound = false;

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
