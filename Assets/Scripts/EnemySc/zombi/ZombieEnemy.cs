using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieEnemy : BaseEnemy
{
    public GameObject deathEffect;
    public float timer;
    private Rigidbody2D rb2D;
    private Animator anim;
    public float knockBackForceX, knockBackForceY;
    public AudioSource enemyHitAS, deadAS;
    public Transform player;
    HitEffect effect;
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        effect = GetComponent<HitEffect>();
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        AudioManager.Instance.PlayAudio(enemyHitAS);
        if (player.position.x < transform.position.x)
        {
            rb2D.AddForce(new Vector2(knockBackForceX, knockBackForceY), ForceMode2D.Force);
        }
        else
        {
            rb2D.AddForce(new Vector2(-knockBackForceX, knockBackForceY), ForceMode2D.Force);
        }


        GetComponent<SpriteRenderer>().material = effect.white;
        StartCoroutine(BackToNormal());
        
    }
    public override void Death()
    {
        base.Death();

        Instantiate(deathEffect, transform.position, transform.rotation);

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



        Destroy(gameObject);
        Experience.instance.expMod(experienceToGive);
        AudioManager.Instance.PlayAudio(deadAS);
    }
    IEnumerator BackToNormal()
    {
        yield return new WaitForSeconds(timer);
        GetComponent<SpriteRenderer>().material = effect.Original;

    }
}
