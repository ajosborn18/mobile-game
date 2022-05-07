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
    public string levelToLoad;
    public Transform feet; 
    public Vector2 startpos;
    public TextMeshProUGUI score;
    public float bouncyForce = 550;
    bool grounded = false; 
    float groundCheckDist = 0.3f;
    bool isAlive = true;

    Animator anim;

    
    //check if the player is stuck in either x or y direction
    Vector3 lastCheckPos;
    float lastCheckTime = 0;
    float checkTimeInterval = 3.0f;
    float xDist = 0.5f;
    float yDist = 0.5f;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        startpos = transform.position;
        lastCheckPos = transform.position;
        //rb.velocity = new Vector2(transform.localScale.x * speed, 0);
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(feet.position, groundCheckDist, ground);
        /*
        if (!grounded) {
            print("jumping");
        } */
		anim.SetBool("grounded", grounded);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(transform.localScale.x * speed, rb.velocity.y);


        /*
        if ((Time.time - lastCheckTime) > checkTimeInterval) {
            if (((transform.position.x - lastCheckPos.x) < xDist)
            || ((transform.position.y - lastCheckPos.y) < yDist)) {
                print("stagnant");
                float x = transform.position.x;
                float y = transform.position.y;
                transform.position = new Vector2(x, (y-5));
                rb.AddForce(new Vector2(0, -200)); 
            }
            lastCheckPos = transform.position;
            lastCheckTime = Time.time;
        } */

        //if not grounded and not jumping, add down force
        if (grounded)
        {
            //anim.SetBool("jumping", false);
            if (Input.GetButtonDown("Jump") || Input.touchCount > 0) {
                rb.AddForce(new Vector2(0, jumpForce));
                //anim.SetBool("jumping", true);
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
            SceneManager.LoadScene(levelToLoad);
            //rb.velocity = new Vector2(transform.localScale.x * speed, 0);
            isAlive = true;
        } 
        if(rb.velocity.y == 0)
        {
            grounded = true;
        }
    }

    void Respawn() {
        
            PublicVars.score = 0;
            //rb.velocity = new Vector2(transform.localScale.x * speed, 0);
            score.text = "0";
    }

    private void OnTriggerEnter2D(Collider2D other) {   
        print("colliding");
        if (other.CompareTag("Collectible")) {
            PublicVars.Collect();
            score.text = (PublicVars.score).ToString();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Collectible2")) {
            PublicVars.Collect2();
            score.text = (PublicVars.score).ToString();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("bouncy")) {
            rb.AddForce(new Vector2(0, bouncyForce));
        }
        else if (other.CompareTag("Obstacle")) {
            isAlive = false;
        }
    }
    
}
