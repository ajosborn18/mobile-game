using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    Rigidbody2D rb;
    public int speed = 7;
    public int jumpForce = 1000;
    public int fallSpeed = -5;
    public LayerMask ground;
    public Transform feet; 
    public Vector2 startpos;
    bool grounded = false; 
    float groundCheckDist = 0.3f;
    //bool isAlive = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startpos = transform.position;
        //rb.velocity = new Vector2(transform.localScale.x * speed, 0);
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(feet.position, groundCheckDist, ground);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(transform.localScale.x * speed, rb.velocity.y);
        //if not grounded and not jumping, add down force
        if (grounded)
        {
            if (Input.GetButtonDown("Jump") || Input.touchCount > 0) {
                rb.AddForce(new Vector2(0, jumpForce));
                grounded = false;
            }
        }
        if (transform.position.y < -10) {
            transform.position = startpos;
        }
        if(rb.velocity.y == 0)
        {
            grounded = true;
        }
    }

    
}
