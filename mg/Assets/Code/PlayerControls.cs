using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerControls : MonoBehaviour
{
    Rigidbody2D rb;
    public int speed = 7;
    public int jumpForce = 1000;
    public int fallSpeed = -5;
    public LayerMask ground;
    public Transform feet; 
    public Vector2 startpos;
    public TextMeshProUGUI score;
    bool grounded = false; 
    float groundCheckDist = 0.3f;
    bool isAlive = true;

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
                rb.AddForce(new Vector2(0, PublicVars.jumpForce));
                grounded = false;
            }
        }
        if (transform.position.y < -10) {
            /*
            transform.position = startpos;
            PublicVars.score = 0;
            score.text = "0"; */
            isAlive = false;
        }
        
        if (!isAlive) {
            Respawn();
            isAlive = true;
        } 
        if(rb.velocity.y == 0)
        {
            grounded = true;
        }
    }

    void Respawn() {
        transform.position = startpos;
            PublicVars.score = 0;
            score.text = "0";
    }

    private void OnTriggerEnter2D(Collider2D other) {   
        print("colliding");
        if (other.CompareTag("Collectible")) {
            PublicVars.Collect();
            score.text = (PublicVars.score).ToString();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Obstacle")) {
            isAlive = false;
        }
    }
    
}
