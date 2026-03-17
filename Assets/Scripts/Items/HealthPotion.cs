using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public float healthToGive;

    GameManagerTwo gameManager;
    Inventory inventory;
    public GameObject itemToAdd;
    public int itemAmount;

    private void Start()
    {
        gameManager = GameManagerTwo.instance;
        inventory=gameManager.GetComponent<Inventory>();    
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inventory.CheckSlotsAvaibleity(itemToAdd,itemToAdd.name,itemAmount);    
            collision.GetComponent<PlayerHealth>().currentHealth += healthToGive;
            Destroy(gameObject);
        }
    }


}
