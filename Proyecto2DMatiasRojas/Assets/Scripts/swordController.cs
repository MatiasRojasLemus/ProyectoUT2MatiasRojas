using UnityEngine;

public class swordController : MonoBehaviour
{
    public Transform transSword;
    public float speedSword = 10f;
    private Vector2 arrowDirection;

    // Start is called before the first frame update
    void Start()
    {
        speedSword = 10f;
        if(arrowDirection == Vector2.right){
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else{
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transSword.Translate(arrowDirection * speedSword * Time.fixedDeltaTime);
    }

    public void setDirection(Vector2 direccion){
        arrowDirection = direccion;
    }
}
