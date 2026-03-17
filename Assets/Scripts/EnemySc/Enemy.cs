using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Animator anim;

    public float maxHealth;
    private float currentHealth;
    public GameObject deathEffect;
    public float timer;
    HitEffect effect;
    public float knockBackForceX, knockBackForceY;
    public Transform player;
    public float damage;

    public float expToGive;

    public AudioSource enemyHitAS, deadAS;
    public GameObject[] lootItems;

    
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        effect = GetComponent<HitEffect>();
    }

    
    void Update()
    {
        
    }
  
    


    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
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
        
        if (currentHealth <= 0) 
        {
            currentHealth = 0;
           
            Instantiate(deathEffect, transform.position, transform.rotation);

            int lootChance = Random.Range(0, 101);
            int loots=Random.Range(1,lootItems.Length+1);
            if (lootChance > 10) 
            {
                Instantiate(lootItems[0], transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(lootItems[loots], transform.position, Quaternion.identity);
            }

                
            
            Destroy(gameObject);
            Experience.instance.expMod(expToGive);
            AudioManager.Instance.PlayAudio(deadAS);

        }
    }
    IEnumerator BackToNormal()
    {
        yield return new WaitForSeconds(timer);
        GetComponent<SpriteRenderer>().material = effect.Original;
            
    }
}
