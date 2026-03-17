using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public float maxHealth;
    
    public float currenHealth;
    public float damage;
    public float experienceToGive;


    public GameObject[] lootItems;
    private void Awake()
    {
        currenHealth = maxHealth;
    }
    void Start()
    {
        currenHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void TakeDamage(float damage)
    {
        currenHealth -= damage; 

        if (currenHealth <= 0)
        {
            currenHealth = 0;
            Death();
        }
    }
    public virtual void Death()
    {

    }
}
