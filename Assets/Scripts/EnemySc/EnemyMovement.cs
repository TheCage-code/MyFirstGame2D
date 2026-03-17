using System.Collections;
using UnityEngine.Tilemaps;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public bool isStatic;
    public bool isWalker;
    public bool isPatroller;
    public bool isWalkingRight;
    private Animator anim;
    private Rigidbody2D rb2D;

    public Transform wallCheck;
    public Transform groundCheck;
    public Transform gapCheck;
    public bool wallDetected, groundDetected, gapDetected;
    public float detectionRaidus;
    public LayerMask whatIsGround;
    public Transform pointA,pointB;
    private bool moveA,moveB;
    public bool waiter;
    private float waitTime = 1;
     bool isWaiting;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        moveA = true;
    }

    // Update is called once per frame
    void Update()
    {
        gapDetected = !Physics2D.OverlapCircle(gapCheck.position, detectionRaidus, whatIsGround);
        wallDetected = Physics2D.OverlapCircle(wallCheck.position, detectionRaidus, whatIsGround);
        groundDetected = Physics2D.OverlapCircle(groundCheck.position, detectionRaidus, whatIsGround);


        if (gapDetected || wallDetected&&groundDetected)
        {
            Flip();
        }
    }
    private void FixedUpdate()
    {
        if (isStatic)
        {
            anim.SetBool("Idle", true);
            rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        if (isWalker)
        {
            rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            anim.SetBool("Idle", false);
            if (!isWalkingRight)
            {
                rb2D.linearVelocity = new Vector2(-speed * Time.deltaTime, rb2D.linearVelocity.y);
            }
            else
            {
                rb2D.linearVelocity = new Vector2(speed * Time.deltaTime, rb2D.linearVelocity.y);
            }
        }
        if (isPatroller)
        {
            
            if (moveA)
            {
                if (!isWaiting)
                {
                    
                    rb2D.linearVelocity = new Vector2(-speed * Time.deltaTime, rb2D.linearVelocity.y);
                    anim.SetBool("Idle", false);
                }
                
                if (Vector2.Distance(transform.position, pointA.position) < 0.2f)                    
                {
                    if(waiter)
                    { StartCoroutine(Waiting()); }
                    
                    Flip();
                    moveA = false;
                    moveB = true;
                }
            }
            if (moveB)
            {
                if (!isWaiting)
                {
                    rb2D.linearVelocity = new Vector2(speed * Time.deltaTime, rb2D.linearVelocity.y);
                    anim.SetBool("Idle", false);
                }
                
                if (Vector2.Distance(transform.position, pointB.position) < 0.2f)
                {
                    if (waiter)
                    { 
                        StartCoroutine(Waiting()); 
                    }
                    Flip();
                    moveA = true;
                    moveB = false;
                }
            }

        }
    }


    public void Flip()
    {
        isWalkingRight = !isWalkingRight;
        Vector3 thescale = transform.localScale;
        thescale.x *= -1;
        transform.localScale = thescale;
    }

    IEnumerator Waiting()
    {
        anim.SetBool("Idle", true);
        isWaiting = true;
        Flip();
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
        anim.SetBool("Idle", false);
        Flip();
    }

    }
