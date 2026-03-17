using UnityEngine;

public class FireBallScript : MonoBehaviour
{
    public float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth.instance.currentHealth-= damage;   
            Destroy(gameObject);
        }
    }
}
