using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayer : MonoBehaviour
{
    public float speed = 4.5f;
    public float jumpForce = 12.0f;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D box;


    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        Vector2 movement = new Vector2(deltaX, body.velocity.y);
        body.velocity = movement;

        // checks if grounded for jump
        box = GetComponent<BoxCollider2D>();
        Vector3 max = box.bounds.max;
        Vector3 min = box.bounds.min;
        Vector2 corner1 = new Vector2(max.x, min.y - .1f); // checks below the collider's min Y values on the right corner (i think)
        Vector2 corner2 = new Vector2(min.x, min.y - .2f); // checks below the collider's min Y values on the left corner (i think)
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

        bool grounded = false;
        if(hit != null) { // if there is a collider below the player
            grounded = true;
        }

        // disables gravity when grounded and idle
        body.gravityScale = (grounded && Mathf.Approximately(deltaX, 0)) ? 0 : 1;
        
        // jump
        if(grounded && Input.GetKeyDown(KeyCode.Space)) {
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // adds a force upwards as an impulse
        }


        // animation
        anim.SetFloat("speed", Mathf.Abs(deltaX));
        if(!Mathf.Approximately(deltaX, 0)) { // floats aren't always exact, so compare using Approximately()
            transform.localScale = new Vector3(Mathf.Sign(deltaX), 1, 1); // when moving, scales positive or negative 1 to face right or left
        }
    }
}
