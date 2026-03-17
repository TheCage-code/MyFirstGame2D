using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    private Enemy_AI enemy;
    void Start()
    {
        enemy= GetComponentInParent<Enemy_AI>();    
    }

   
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameObject.SetActive(false);
            enemy.target = collision.transform;
            enemy.inRange = true;
            enemy.actionZone.SetActive(true);
        }
    }
}
