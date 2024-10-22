using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SpriteRenderer sprtRnd;
    public Rigidbody2D rb;
    public float speedX;
    public float speedY;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(speedX, speedY);

        if (Input.GetKeyDown(KeyCode.A))
        {
            sprtRnd.flipX = true;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {   
            sprtRnd.flipX = false;
        }
    }
}
