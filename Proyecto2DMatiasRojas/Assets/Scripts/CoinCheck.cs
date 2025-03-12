using UnityEngine;

public class CoinCheck : MonoBehaviour
{
    public SpriteRenderer spriteCoin;
    public AudioSource audioMonedaRecogida;
    private float timeWhenTouched;
    private float timeAudio = 0.2f;

    
    void FixedUpdate(){
        Espera();
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            timeWhenTouched = Time.time;
            audioMonedaRecogida.Play();
        }
    }

    public void Espera(){
        if(timeWhenTouched > 0 && Time.time < timeWhenTouched + timeAudio){
            Object.Destroy(gameObject);
        }
    }
}
