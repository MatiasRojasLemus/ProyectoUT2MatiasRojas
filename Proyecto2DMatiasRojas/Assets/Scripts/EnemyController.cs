using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speedMove = 3f;
    private SpriteRenderer sprtRnd;
    private Rigidbody2D rb2D;
    private Transform playerTransform;
    private Transform enemyTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        sprtRnd = GetComponent<SpriteRenderer>();
        enemyTransform = GetComponent<Transform>();
        rb2D = GetComponent<Rigidbody2D>();
        playerTransform =  GameObject.FindWithTag("Player").GetComponent<Transform>();
        speedMove = 3f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(EnemyCheckPlayer.playerFound){
            if(enemyTransform.position.x < playerTransform.position.x){
                sprtRnd.flipX = false;
                rb2D.velocity = new Vector2(speedMove, rb2D.velocity.y);
            }
            else if(enemyTransform.position.x > playerTransform.position.x){
                sprtRnd.flipX = true;
                rb2D.velocity = new Vector2(-speedMove, rb2D.velocity.y);
            }
            else{
                rb2D.velocity = new Vector2(0, rb2D.velocity.y);
            }
        }
    }
}
