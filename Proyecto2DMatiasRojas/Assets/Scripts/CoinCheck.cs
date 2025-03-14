using UnityEngine;


//Codigo enfocado en el funcionamiento de las monedas que hay repartidas por el mapa.
public class CoinCheck : MonoBehaviour
{
    public SpriteRenderer spriteCoin;
    public AudioSource audioMonedaRecogida;
    private float timeWhenTouched;
    private const float timeAudio = 0.2f;

    
    void FixedUpdate(){
        Espera();
    }

    /* Reproduce un audio tras su contacto con el Player. Tambien 
    toma registro del tiempo exacto de cuando ha sido tocado.
    */
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            timeWhenTouched = Time.time;
            audioMonedaRecogida.Play();
        }
    }

    /*
    * Mide el lapso del tiempo desde que el jugador ha cogido la moneda y esta haya reproducido su audio.
    * Es un metodo utilizado para destruir el objeto despues de que haya reproducido su audio en el metodo anterior.
    */
    public void Espera(){
        if(timeWhenTouched > 0 && Time.time < timeWhenTouched + timeAudio){
            Object.Destroy(gameObject);
        }
    }
}
