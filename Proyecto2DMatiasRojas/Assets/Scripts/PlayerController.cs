using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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
    private float cooldownAttack = 0.25f;
    private bool isFacingRight;
    private float timeWhenLastAttack;

    void Start()
    {
        rb.gravityScale = 1f;
        speedMove = 7f;
        jumpingHeight = 7f;
    }

    void FixedUpdate()
    {
        CheckMovement();
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
}
