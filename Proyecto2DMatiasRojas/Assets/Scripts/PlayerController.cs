using UnityEngine;

/*
    Codigo que define el comportamiento completo del objeto Player:
    -   El control del usuario sobre el Player
    -   Su movimiento, tanto en suelo como en el aire
    -   Animaciones
    -   Lanzamiento de proyectiles
    -   Interacciones con el escenario
    -   Interacciones con los enemigos
    -   Daño, muerte y respawn del Player
*/
public class PlayerController : MonoBehaviour
{
    //COMPONENTES DEL PLAYER.
    public Rigidbody2D rb;
    public SpriteRenderer sprtRnd;
    public Animator anim;
    public Transform transPlayer;
    public GameObject swordPrefab;
    public AudioSource audioHurt;
    public AudioSource audioJump;


    //ATRIBUTO CON RESPECTO A SUS FISICAS Y MOVIMIENTO.
    public Vector2 direccionSword;
    public float speedMove;
    public float jumpingHeight;

    //ATRIBUTOS CON RESPECTO AL PLAYER ATACANDO.
    private const float cooldownAttack = 0.2f;
    private float timeWhenLastAttack;


    //ATRIBUTOS CON RESPECTO AL PLAYER RECIBIENDO DAÑO.
    private const float initLifes = 5f;
    private float timeWhenLastHit;
    private float lifes;
    private bool isDead = false;
    private const float hitAnimationTime = 0.333f;
    private const float deadAnimationTime = 1.517f;

    //ATRIBUTOS CON RESPECTO A LA POSICION DEL PLAYER.
    private float posicionX;
    private float posicionY;
    private float posicionZ;

    void Start()
    {
        lifes = initLifes;
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
    
    //CheckingGround: Comprueba si esta tocando el suelo o no para activar la animacion de salto.
    private void CheckingGround(){
        if (GroundCheck.isGrounded){
            anim.SetBool("isGrounded", true);
        }
        else{
            anim.SetBool("isGrounded", false);
        }
    }


    //Move: Metodo que define el movimiento del personaje en base a la tecla pulsada.
    private void Move(){
        
        if (Input.GetKey(KeyCode.RightArrow)){
            //Movimiento a la derecha con la flecha derecha.
            sprtRnd.flipX = false;
            anim.SetBool("isRunning", true);
            rb.velocity = new Vector2(speedMove, rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.LeftArrow)){
            //Movimiento a la izquierda con la flecha izquierda.
            sprtRnd.flipX = true;
            anim.SetBool("isRunning", true);
            rb.velocity = new Vector2(-speedMove, rb.velocity.y);
        }
        else {
            //Se queda quieto y hace la animacion de Idle.
            rb.velocity = new Vector2(0, rb.velocity.y);
            anim.SetBool("isRunning", false);
        }


        //Permite saltar al Player y se asegura de que no pueda saltar infinitamente.
        if(Input.GetKey(KeyCode.Space) && GroundCheck.isGrounded){
            audioJump.Play();
            rb.AddForce(Vector3.up * jumpingHeight, ForceMode2D.Impulse);
        }
    }


    //Attack: Metodo que el Player pueda lanzar un proyectil pulsando la tecla X.
    private void Attack(){
        if(Input.GetKey(KeyCode.X)){
            //Define el tiempo minimo entre ataque y ataque.
            if (Time.time < timeWhenLastAttack + cooldownAttack){
                return;
            }
            
            //Inicializa un objeto del prefab "sword".
            GameObject sword = Instantiate(swordPrefab,transPlayer.position,Quaternion.identity);
            
            
            //Dependiendo de la orientacion del Player, se define la direccion del proyectil, izquierda o derecha. 
            if(sprtRnd.flipX){
                direccionSword = Vector2.left;
            }
            else{
                direccionSword = Vector2.right;
                sword.GetComponent<SpriteRenderer>().flipX = true;
            }

            sword.GetComponent<SwordController>().SetDirectionSword(direccionSword);

            timeWhenLastAttack = Time.time;
        }
    }



    //OnTriggerEnter2D: Define las interacciones del personaje con la escena y como le afectan
    public void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Pincho"))
        {
            audioHurt.Play();
            GetHit(1);
        }
        else if(collision.gameObject.CompareTag("EnemyPurple"))
        {
            audioHurt.Play();
            GetHit(2);
        }
        else if(collision.gameObject.CompareTag("PinchoMortal"))
        {
            audioHurt.Play();
            GetHit(10);
        }
        // Si entra en contacto con un Checkpoint, guarda la posición con la que ha entrado a dicho checkpoint
        else if(collision.gameObject.CompareTag("CheckPoint"))
        {
            posicionX = transPlayer.position.x;
            posicionY = transPlayer.position.y;
            posicionZ = transPlayer.position.z;
        }
    }
    


    /* GetHit: Metodo que quita vida al Player y activa la animacion "hit" (golpe).
    Tambien comprueba si esta muerto dependiendo de su salud restante. */
    private void GetHit(float vidasPerdidas){
        anim.SetBool("gotHit",true);
        lifes -= vidasPerdidas;

        if(lifes <= 0){
            SetDeathState(true);
            timeWhenLastHit = Time.time;
        }
        
        timeWhenLastHit = Time.time;
    }


    //AfterHit: Controla el tiempo de la animacion de golpe para que se realice solo una vez.
    private void AfterHit(){
        if(Time.time > timeWhenLastHit + hitAnimationTime){
            anim.SetBool("gotHit",false);
        }
    }


    /* SetDeathState: Define si el estado del personaje es muerto o vivo.
    Si esta muerto, se activa la animacion de muerte. */
    private void SetDeathState(bool isDeadAux){
        isDead = isDeadAux;
        anim.SetBool("isDead",isDeadAux);
    }


    /* Reset: Si el personaje ha muerto y ha pasado el tiempo correspondiente a la animacion de muerte,
    reinicia la posicion del personaje al ultimo checkpoint, vuelve a tener sus vidas iniciales,
    y se normalizan los estados del animator a los iniciales */
    private void Reset(){
        if(isDead && Time.time > timeWhenLastHit + deadAnimationTime){
            ResetPosition();
            ResetLifes();
            anim.SetBool("gotHit",false);
            SetDeathState(false);
        }
    }


    /* ResetPosition: Cambia la posicion del personaje al definido 
    por las variables posicionX, posicionY y posicionZ. */
    private void ResetPosition(){
        transform.position = new Vector3(posicionX,posicionY,posicionZ);
    }


    //ResetLifes: Las vidas del player recuperan su valor inicial.
    private void ResetLifes(){
        lifes = initLifes;
    }


}
