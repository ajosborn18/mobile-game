using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerControls : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 7;
    float initialSpeed;
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
    public ParticleSystem speed_signal;

    Animator anim;
    public AudioSource aud;
    public AudioClip collect;
    public AudioClip die;

    //check if the player is stuck in either x or y direction
    Vector3 lastCheckPos;
    float lastCheckTime = 0;
    float checkTimeInterval = 3.0f;
    float xDist = 0.5f;
    float yDist = 0.5f;

    void Start()
    {
        initialSpeed = speed;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        aud = GetComponent<AudioSource>();
        startpos = transform.position;
        lastCheckPos = transform.position;
        speed_signal.Stop();
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

    IEnumerator LoadNewLevel()
    {
        yield return new WaitForSeconds(0.75f);
        SceneManager.LoadScene(levelToLoad);
    }

    private void restartLevel()
    {
        aud.PlayOneShot(die);
        StartCoroutine(LoadNewLevel());
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
            if (Input.GetButtonDown("Jump") || Input.touchCount > 0)
            {
                rb.AddForce(new Vector2(0, jumpForce));
                //anim.SetBool("jumping", true);
                grounded = false;
            }
        }
        if (transform.position.y < -10)
        {
            /*
            transform.position = startpos;
            PublicVars.score = 0;
            score.text = "0"; */
            isAlive = false;
        }

        if (!isAlive)
        {
            Respawn();

            restartLevel();

            //SceneManager.LoadScene(levelToLoad);
            //rb.velocity = new Vector2(transform.localScale.x * speed, 0);
            isAlive = true;
        }
        if (rb.velocity.y == 0)
        {
            grounded = true;
        }
    }

    void Respawn()
    {
        
        PublicVars.score = 0;
        //rb.velocity = new Vector2(transform.localScale.x * speed, 0);
        score.text = "0";
        
    }

    IEnumerator IncreaseSpeed(float multiplier)
    {
        speed_signal.Play();
        speed *= multiplier;
        yield return new WaitForSeconds(2);
        speed = initialSpeed; //restore to normal
        speed_signal.Stop();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("colliding");
        if (other.CompareTag("Collectible"))
        {
            aud.PlayOneShot(collect, 0.3f);
            PublicVars.Collect();
            //speed *= 1.25f;
            StartCoroutine(IncreaseSpeed(1.25f));
            score.text = (PublicVars.score).ToString();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Collectible2"))
        {
            aud.PlayOneShot(collect, 0.3f);
            PublicVars.Collect2();
            //speed *= 1.5f;
            StartCoroutine(IncreaseSpeed(1.5f));
            score.text = (PublicVars.score).ToString();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("bouncy"))
        {
            rb.AddForce(new Vector2(0, bouncyForce));
        }
        else if (other.CompareTag("Obstacle"))
        {
            //aud.PlayOneShot(die, 0.3f);
            isAlive = false;
        }
        else if (other.CompareTag("Poison"))
        {
            print("POINSONED");
            PublicVars.score -= 2;
            score.text = (PublicVars.score).ToString();
        }
    }

  
}
