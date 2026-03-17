using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BossSkill : MonoBehaviour
{
    public float damage;
    public Vector2 dimension;
    public Transform positions;
    public float lifeTimeOfSPell;
    void Start()
    {
        Destroy(gameObject, lifeTimeOfSPell);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Skill()
    {
        Collider2D[] objects = Physics2D.OverlapBoxAll(positions.position, dimension, 0f);
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
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(positions.position, dimension);
    }
}
