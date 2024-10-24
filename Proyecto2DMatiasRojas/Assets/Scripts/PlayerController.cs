using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speedMove;
    public float jumpingPower;
    public SpriteRenderer sprtRnd;


    private float horizontal;
    private bool isFacingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        checkMovement();
    }

    public void checkMovement(){
        rb.velocity = new Vector2(horizontal * speedMove, rb.velocity.y);
        
        if(!isFacingRight && horizontal > 0f){
             //Girar a la derecha
            isFacingRight = true;
            sprtRnd.flipX = false;
        }
        else if(isFacingRight && horizontal < 0f){
            //Girar a la izquierda
            isFacingRight = false;
            sprtRnd.flipX = true;
        }
    }

    public void Move(InputAction.CallbackContext context){
        horizontal = context.ReadValue<Vector2>().x;
        Debug.Log(horizontal); 
    }
}
