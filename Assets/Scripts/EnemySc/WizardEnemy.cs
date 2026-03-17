using UnityEngine;

public class WizardEnemy : BaseEnemy
{
    public GameObject fireBall;
    public float timeTOShoot;
    public float shootCoolDown;
    public float fireBallSpeed;

    public bool freqShooter;
    public bool isWatcher;

    private Animator anim;
    void Start()
    {
        shootCoolDown = timeTOShoot;
        anim= GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if ((freqShooter))
        {
            shootCoolDown -= Time.deltaTime;
            if (shootCoolDown < 0) 
            { 
                Shoot(); 
            }
            
        }
        if (isWatcher)
        {
            
        }
          
        
        
    }
    public void Shoot()
    {
        anim.SetTrigger("Attack");
        GameObject fireball = Instantiate(fireBall, transform.position, Quaternion.identity);

        if (transform.localScale.x < 0)
        {
            fireball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-fireBallSpeed, 0f), ForceMode2D.Force);
        }
        else
        {
            fireball.GetComponent<Rigidbody2D>().AddForce(new Vector2(fireBallSpeed, 0f), ForceMode2D.Force);
        }
        shootCoolDown = timeTOShoot;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        anim.SetTrigger("Hit");
    }
    public override void Death()
    {
        base.Death();
        int lootChance = Random.Range(0, 101);
        int loots = Random.Range(1, lootItems.Length + 1);
        if (lootChance > 10)
        {
            Instantiate(lootItems[0], transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(lootItems[loots], transform.position, Quaternion.identity);
        }

        anim.SetTrigger("Death");
        Destroy(gameObject,0.8f);
    }
}
