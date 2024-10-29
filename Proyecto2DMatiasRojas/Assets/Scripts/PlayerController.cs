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
    
    private float cooldownAttack = 0.2f;
    private float horizontal;
    private bool isFacingRight = true;
    private float timeWhenLastAttack;

    public InputAction.CallbackContext context;

    void Start()
    {
        rb.gravityScale = 3f;
        speedMove = 7f;
        jumpingHeight = 7f;
    }

    void FixedUpdate()
    {
        checkMovement();
    }

    public void checkMovement()
    {
        rb.velocity = new Vector2(horizontal * speedMove, rb.velocity.y);


        if (Mathf.Abs(horizontal) == 0f)
        {
            //Si no se mueve
            anim.SetBool("isRunning", false);
        }
        else
        {
            //Si se mueve
            anim.SetBool("isRunning", true);
        }


        if (GroundCheck.isGrounded)
        {
            anim.SetBool("isGrounded", true);
        }
        else
        {
            anim.SetBool("isGrounded", false);
        }


        if (!isFacingRight && horizontal > 0f)
        {
            isFacingRight = true;
            sprtRnd.flipX = false;
        }
        else if (isFacingRight && horizontal < 0f)
        {
            isFacingRight = false;
            sprtRnd.flipX = true;
        }
    }

    public void Move(InputAction.CallbackContext context){
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context){
        if(GroundCheck.isGrounded){ 
            rb.AddForce(Vector3.up * jumpingHeight, ForceMode2D.Impulse);
        }
    }

    public void Attack(InputAction.CallbackContext context){
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
