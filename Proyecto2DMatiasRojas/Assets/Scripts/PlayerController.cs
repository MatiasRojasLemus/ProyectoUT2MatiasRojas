using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sprtRnd;
    public Animator anim;
    public Transform transPlayer;
    public GameObject swordPrefab;
    public AudioSource audioHurt;
    public AudioSource audioJump;

    public Vector2 direccionSword;
    public float speedMove;
    public float jumpingHeight;
    private float cooldownAttack = 0.2f;
    private bool isFacingRight = false;
    private float timeWhenLastAttack;


    //ATRIBUTOS CON RESPECTO AL JUGADOR RECIBIENDO DAÑO
    private float timeWhenLastHit;
    private float lifes = 5f;
    private float hitAnimationTime = 0.333f;
    private bool isDead = false;
    private float deadAnimationTime = 1.517f;

    //CheckPoint
    private float posicionX;
    private float posicionY;
    private float posicionZ;

    void Start()
    {
        rb.gravityScale = 1f;
        speedMove = 10f;
        jumpingHeight = 8f;
        audioHurt.GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        CheckMovement();
        Reset();
    }


    private void CheckMovement(){
        if(!isDead){
            CheckingGround();
            Move();
            Attack();
            AfterHit();
        }
    }
    
    private void CheckingGround(){
        if (GroundCheck.isGrounded)
        {
            anim.SetBool("isGrounded", true);
        }
        else
        {
            anim.SetBool("isGrounded", false);
        }
    }

    private void Move(){
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
            audioJump.Play();
            rb.AddForce(Vector3.up * jumpingHeight, ForceMode2D.Impulse);
        }
    }


    private void Attack(){
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

    //Interacciones del personaje con la escena
    public void OnTriggerEnter2D(Collider2D collision){
        //Si entra en contacto con un enemigo
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Pincho"){
            audioHurt.Play();
            getHit(1);
        }
        // Si entra en contacto con un Checkpoint, guarda la posición con la que ha entrado a dicho checkpoint
        else if(collision.gameObject.tag == "CheckPoint"){
            posicionX = transPlayer.position.x;
            posicionY = transPlayer.position.y;
            posicionZ = transPlayer.position.z;
        }
        else if(collision.gameObject.tag == "EnemyPurple"){
            audioHurt.Play();
            getHit(1);
        }
        else if(collision.gameObject.tag == "PinchoMortal"){
            audioHurt.Play();
            getHit(10);
        }
    }
    

    private void getHit(float vidaPerdida){
        anim.SetBool("gotHit",true);
        lifes -= vidaPerdida;

        if(lifes <= 0){
            setDeathState(true);
            timeWhenLastHit = Time.time;
        }
        
        timeWhenLastHit = Time.time;
    }

    private void AfterHit(){
        if(Time.time > timeWhenLastHit + hitAnimationTime){
            anim.SetBool("gotHit",false);
        }
    }

    private void Reset(){
        if(isDead && Time.time > timeWhenLastHit + deadAnimationTime){
            ResetPosition();
            ResetLifes();
            anim.SetBool("gotHit",false);
            setDeathState(false);
        }
    }


    private void ResetPosition(){
        transform.position = new Vector3(posicionX,posicionY,posicionZ);
    }

    private void ResetLifes(){
        lifes = 5;
    }

    private void setDeathState(bool isDeadAux){
        isDead = isDeadAux;
        anim.SetBool("isDead",isDeadAux);
    }
}
