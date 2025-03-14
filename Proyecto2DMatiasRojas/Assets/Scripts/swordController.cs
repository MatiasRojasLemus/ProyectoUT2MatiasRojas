using UnityEngine;

//Codigo que define el comportamiento de los proyectiles que lanza el Player.
public class SwordController : MonoBehaviour
{
    public Transform transSword;
    public float speedSword = 12f;
    private Vector2 arrowDirection;
    private float currentTimeAlive = 0f;
    private const float lifeSword = 3f;

    void FixedUpdate()
    {
        MovementSword();
    }

    /* MovementSword: Define la velocidad del proyectil, el aspecto de su sprite
    y su tiempo de vida restante en la escena.*/
    private void MovementSword()
    {
        transSword.Translate(speedSword * Time.fixedDeltaTime * arrowDirection);

        //Ajusta el aspecto del sprite en base a la direccion a la que vaya dirigido el proyectil.
        if (arrowDirection == Vector2.right){
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else{
            GetComponent<SpriteRenderer>().flipX = true;
        }

        currentTimeAlive += Time.fixedDeltaTime;

        if (currentTimeAlive >= lifeSword){
            Object.Destroy(gameObject);
        }

    }

    //OnTriggerEnter2D: Hace desaparecer el proyectil de la escena si entra en contacto con el enemigo o el escenario.
    public void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.CompareTag("Enemy") || collider.gameObject.CompareTag("Suelo")){
            EnemyController.gotHit = true;
            Object.Destroy(gameObject);
        }
    }
    
    //Define la direccion a la que se va a lanzar el proyectil dependiendo de la direccion a la que apunte el player.
    public void SetDirectionSword(Vector2 direccion){
        arrowDirection = direccion;
    }

}
