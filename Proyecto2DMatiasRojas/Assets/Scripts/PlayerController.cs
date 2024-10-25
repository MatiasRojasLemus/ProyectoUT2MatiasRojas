using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speedMove;
    public float jumpingPower;
    public SpriteRenderer sprtRnd;
    public Animator anim;
    private float horizontal;
    private bool isFacingRight = true;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        checkMovement();
    }

    public void checkMovement(){
        if(Mathf.Abs(horizontal) == 0f){
            //Si no se mueve
            anim.SetBool("isRunning",false);
        }
        else{
            //Si se mueve
            anim.SetBool("isRunning",true);
        }

        rb.velocity = new Vector2(horizontal * speedMove, rb.velocity.y);
        
        if(!isFacingRight && horizontal > 0f){
            isFacingRight = true;
            sprtRnd.flipX = false;
        }
        else if(isFacingRight && horizontal < 0f){
            isFacingRight = false;
            sprtRnd.flipX = true;
        }
    }

    public void Move(InputAction.CallbackContext context){
        horizontal = context.ReadValue<Vector2>().x;
        Debug.Log(horizontal); 
    }

}
