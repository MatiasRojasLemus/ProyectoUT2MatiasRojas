using UnityEngine;
using UnityEngine.SceneManagement;


//Clase para gestionar los cambios de escena
public class SceneController : MonoBehaviour
{
    public string escenaACargar;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(escenaACargar);
        }  
    }
}
