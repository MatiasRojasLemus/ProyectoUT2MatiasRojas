using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sprtRnd;
    public Animator anim;
    public Transform transPlayer;
    public GameObject swordPrefab;

    public Vector2 direccionSword;
    public float speedMove;
    public float jumpingHeight;
    private float cooldownAttack = 0.3f;
    private bool isFacingRight = false;
    private float timeWhenLastAttack;


    //ATRIBUTOS CON RESPECTO AL JUGADOR RECIBIENDO DAÑO
    private float timeWhenLastHit;
    private float lifes = 5f;
    private float hitAnimationTime = 0.333f;
    private float invulnerabilityWhenHit = 120f;
    private float recoil = 20f;
    private bool isDead = false;
    private float deadAnimationTime = 1.517f;

    void Start()
    {
        rb.gravityScale = 1f;
        speedMove = 10f;
        jumpingHeight = 8f;
    }

    void FixedUpdate()
    {
        CheckMovement();
        AfterHit();
    }


    public void CheckMovement(){
        CheckingGround();
        Move();
        Attack();
    }
    
    public void CheckingGround(){
        if (GroundCheck.isGrounded)
        {
            anim.SetBool("isGrounded", true);
        }
        else
        {
            anim.SetBool("isGrounded", false);
        }
    }

    public void Move(){
        if (Input.GetKey(KeyCode.RightArrow))
        {
            isFacingRight = true;
            sprtRnd.flipX = false;
            anim.SetBool("isRunning", true);
            rb.velocity = new Vector2(speedMove, rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            isFacingRight = false;
            sprtRnd.flipX = true;
            anim.SetBool("isRunning", true);
            rb.velocity = new Vector2(-speedMove, rb.velocity.y);
        }
        else{
            rb.velocity = new Vector2(0, rb.velocity.y);
            anim.SetBool("isRunning", false);
        }

        if(Input.GetKey(KeyCode.Space) && GroundCheck.isGrounded){ 
            rb.AddForce(Vector3.up * jumpingHeight, ForceMode2D.Impulse);
        }
    }


    public void Attack(){
        if(Input.GetKey(KeyCode.X)){
            if (Time.time < timeWhenLastAttack + cooldownAttack)
            {
                return;
            }
            
            GameObject sword = Instantiate(swordPrefab,transPlayer.position,Quaternion.identity);
            
            if(sprtRnd.flipX){
                direccionSword = Vector2.left;
            }
            else{
                direccionSword = Vector2.right;
                sword.GetComponent<SpriteRenderer>().flipX = true;
            }

            sword.GetComponent<swordController>().setDirectionSword(direccionSword);

            timeWhenLastAttack = Time.time;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Enemy" && Time.time < timeWhenLastHit + invulnerabilityWhenHit){
            getHit();
        }
    }

    public void getHit(){
        lifes--;
        anim.SetBool("gotHit",true);

        if(lifes == 0){
            isDead = true;
            anim.SetBool("isDead",true);
            return;
        }

        if(isFacingRight){
            rb.AddForce(Vector3.left * recoil, ForceMode2D.Impulse);
        }
        else{
            rb.AddForce(Vector3.right * recoil, ForceMode2D.Impulse);        
        }

        timeWhenLastHit = Time.deltaTime;
    }

    public void AfterHit(){
        if(Time.time > timeWhenLastHit + hitAnimationTime){
            anim.SetBool("gotHit",false);
        }
    }

    public void Reset(){
        if(isDead){
            if(Time.time > timeWhenLastHit + deadAnimationTime){
                
            }
        }
    }
}
