using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayer : MonoBehaviour
{
    public float speed = 4.5f;
    public float jumpForce = 12.0f;

    private Rigidbody2D body;
    private Animator anim;

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

        // jump
        if(Input.GetKeyDown(KeyCode.Up)) {
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); //
        }
        
        // animation
        anim.SetFloat("speed", Mathf.Abs(deltaX)); 
        if(!Mathf.Approximately(deltaX, 0)) { // floats aren't always exact, so compare using Approximately()
            transform.localScale = new Vector3(Mathf.Sign(deltaX), 1, 1); // when moving, scales positive or negative 1 to face right or left
        }
    }
}
