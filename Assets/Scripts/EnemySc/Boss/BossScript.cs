//using UnityEditor.Tilemaps;
using UnityEngine.Tilemaps;
using UnityEngine;

public class BossScript : BaseEnemy
{
    public Transform player;
    public bool lookingRight = true;

    Animator anim;
   public Rigidbody2D rb;
    public LayerMask playerLayer;

    [Header("Attack")]
    public Transform attack;
    public float attackRadius;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();  
    }

    
    void Update()
    {
        //LookPlayer();
        float distanceToPlayer=Vector2.Distance(transform.position, player.position);
        anim.SetFloat("Distance",distanceToPlayer);
    }
    public void LookPlayer()
    {
        if ((player.position.x > transform.position.x) && !lookingRight || player.position.x < transform.position.x && lookingRight)
        {
            flip();
        }
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }
    public override void Death()
    {
            base.Death();
        anim.SetTrigger("Death");
        Destroy(gameObject,1f);    
    }
    void flip()
    {
        lookingRight = !lookingRight ;
        Vector3 thescale = transform.localScale;
        thescale.x *= -1;
        transform.localScale = thescale;
    }
    public void Attack()
    {
        Collider2D[]objects=Physics2D.OverlapCircleAll(attack.position, attackRadius,playerLayer);   
        foreach(Collider2D collision in objects)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerHealth>().TakeDamage(damage);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attack.position, attackRadius);
    }
}
