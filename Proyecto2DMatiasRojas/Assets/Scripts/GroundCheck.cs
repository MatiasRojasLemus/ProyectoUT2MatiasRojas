using UnityEngine;


//Codigo utilizado para detectar el contacto con el suelo del Player.
public class GroundCheck : MonoBehaviour
{
    public static bool isGrounded;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Suelo")){
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Suelo")){
            isGrounded = false;
        }
    }
}
