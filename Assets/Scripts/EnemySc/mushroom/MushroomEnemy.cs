using System.Collections;
using UnityEngine;

public class MushroomEnemy : BaseEnemy
{
    private Animator anim;
    private Rigidbody2D rb2D;
    public float speed;
    bool isWaiting;
    public bool waiter;
    private float waitTime = 1;
    public Transform pointA, pointB;
    private bool moveA, moveB;
    public bool isWalkingRight;
    public bool isStatic;
    public bool isWalker;
    public bool isPatroller;

   public GameObject hitEffect;
    public GameObject deathEffect;
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        moveA = true;
        
    }

    private void FixedUpdate()
    {
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
                    if (waiter)
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
    // Update is called once per frame
    void Update()
    {
        
       
    }
    
    public override void Death()
    {
        base.Death();
        
        Instantiate(deathEffect, transform.position, Quaternion.Euler(0.0f,0.0f,Random.Range(0.0f,360f)));
        Destroy(gameObject);

    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        anim.SetTrigger("Hit");
        Instantiate(hitEffect, transform.position, Quaternion.identity);
       
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
