using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    Rigidbody2D rb;
    public int speed = 7;
    public int jumpForce = 100;
    public int fallSpeed = -5;
    public LayerMask ground;
    public Transform feet; 
    public Vector2 startpos;
    bool grounded = false; 
    float groundCheckDist = 0.3f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startpos = transform.position;
        //rb.velocity = new Vector2(transform.localScale.x * speed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(transform.localScale.x * speed, fallSpeed);
        //if not grounded and not jumping, add down force
        if (grounded) {
            if (Input.touchCount > 0) {
                rb.AddForce(new Vector2(0, jumpForce));
            }
        }
        if (transform.position.y < -10) {
            transform.position = startpos;
        }
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(feet.position, groundCheckDist, ground);
    }
}
