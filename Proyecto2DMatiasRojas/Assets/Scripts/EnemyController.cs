using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float speedMove = 6f;
    private SpriteRenderer sprtRnd;
    private Rigidbody2D rb2D;
    private Transform playerTransform;
    private Transform enemyTransform;
    private Animator animEnemy;
    public AudioSource audioExplosion;



    private float timeWhenLastHit;
    private float timeWhenDead;
    private float health = 10f;
    private float transitionTime = 0.1f;
    public static bool gotHit = false;
    public static bool isDead = false;
    


    private float margin = 0.5f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        sprtRnd = GetComponent<SpriteRenderer>();
        enemyTransform = GetComponent<Transform>();
        rb2D = GetComponent<Rigidbody2D>();
        playerTransform =  GameObject.FindWithTag("Player").GetComponent<Transform>();
        animEnemy = GetComponent<Animator>();
        speedMove = 3f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FindingPlayer();
        GettingHit();
    }

    public void FindingPlayer(){
        if(EnemyCheckPlayer.playerFound){
            if(enemyTransform.position.x < playerTransform.position.x - margin){
                sprtRnd.flipX = false;
                rb2D.velocity = new Vector2(speedMove, rb2D.velocity.y);
            }
            else if(enemyTransform.position.x > playerTransform.position.x + margin){
                sprtRnd.flipX = true;
                rb2D.velocity = new Vector2(-speedMove, rb2D.velocity.y);
            }
            else{
                rb2D.velocity = new Vector2(0, rb2D.velocity.y);
            }
        }
    }


    public void GettingHit(){
        if(isDead){
            if(Time.time < timeWhenDead + transitionTime){
                audioExplosion.Play();
                return;
            }
            Object.Destroy(gameObject);
        }

        if(gotHit){
            if (Time.time < timeWhenLastHit + transitionTime){
                animEnemy.SetBool("gotHit",false);
                gotHit = false;
                return;
            }

            animEnemy.SetBool("gotHit",true);

            health--;

            if(health == 0){
                animEnemy.SetBool("isDead",true);
                isDead = true;
                timeWhenDead = Time.time;
            }

            timeWhenLastHit = Time.time;
            return;
        }
    }
}
