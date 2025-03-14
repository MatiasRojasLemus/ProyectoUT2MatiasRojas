using UnityEngine;


//Codigo utilizado para detectar al Player dentro de cierto perimetro.
public class EnemyCheckPlayer : MonoBehaviour
{   
    public static bool playerFound = false;

    public void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Player")){
            playerFound = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Player")){
            playerFound = false;
        }
    }
}
