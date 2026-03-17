using UnityEngine;

public class MushTrigger : MonoBehaviour
{
    MushroomEnemy enemy;
    Animator anim;

    void Start()
    {
        enemy = GetComponentInParent<MushroomEnemy>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemy.speed = 95;
            anim.SetBool("Crazy", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemy.speed = 50;
            anim.SetBool("Crazy", false);
        }
    }
}

