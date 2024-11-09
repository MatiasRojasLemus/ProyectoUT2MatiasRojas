using UnityEngine;

public class CoinCheck : MonoBehaviour
{
    public AudioSource audioMonedaRecogida;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            audioMonedaRecogida.Play();
            Object.Destroy(gameObject);
        }
    }
}
