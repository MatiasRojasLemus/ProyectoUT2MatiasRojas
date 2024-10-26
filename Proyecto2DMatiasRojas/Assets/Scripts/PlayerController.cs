using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sprtRnd;
    public Animator anim;
    public float speedMove;
    public float jumpingHeight;
    private float horizontal;
    private bool isFacingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            rb.AddForce(Vector2.up * jumpingHeight, ForceMode2D.Impulse);
        }
    }

}
