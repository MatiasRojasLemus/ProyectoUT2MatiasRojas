using UnityEngine;

public class swordController : MonoBehaviour
{
    public Transform transSword;
    public float speedSword = 12f;
    private Vector2 arrowDirection;
    private float currentTimeAlive = 0f;
    private float lifeSword = 3f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movementSword();
    }

    private void movementSword()
    {
        transSword.Translate(arrowDirection * speedSword * Time.fixedDeltaTime);

        if (arrowDirection == Vector2.right)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        currentTimeAlive += Time.fixedDeltaTime;

        if (currentTimeAlive >= lifeSword)
        {
            Object.Destroy(gameObject);
        }

    }

    public void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Enemy"){
            EnemyController.gotHit = true;
            Object.Destroy(gameObject);
        }
    }
    
    public void setDirectionSword(Vector2 direccion){
        arrowDirection = direccion;
    }

}
