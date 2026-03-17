using UnityEngine;

using TMPro;





public class NinjaStar : MonoBehaviour
{

    public float speed;
     Rigidbody2D rb;
    public float damage;

    public PlayerController player;
    public GameObject groundEffect;
    public GameObject damageText;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindFirstObjectByType<PlayerController>();
        if (player.transform.localScale.x < 0)
        {
            speed = -speed;
        }
            
    }

    
    void Update()
    {
        rb.linearVelocity = new Vector2(speed,rb.linearVelocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
             damage = Mathf.Round(Random.Range(damage - 5, damage + 5));
           GameObject textDam= Instantiate(damageText,collision.transform.position,Quaternion.identity);
            textDam.GetComponent<TextMeshPro>().SetText(damage.ToString());

            collision.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Ground"))
        {
            Instantiate(groundEffect,transform.position,transform.rotation);
            Destroy(gameObject);
            
        }
    }
}
