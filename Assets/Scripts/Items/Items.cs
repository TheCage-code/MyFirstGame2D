using UnityEngine;

public enum ItemType
{
    USABLE,//POTLAR
    WEAPON,
    ARMOR,
    BOOTS,
    HELMET

}
public class Items : MonoBehaviour
{
    public GameObject itemToAdd;
    public int amountToAdd;
    Inventory inventory;
    GameManagerTwo gameManager;

    void Start()
    {
        gameManager = GameManagerTwo.instance;
        inventory = gameManager.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inventory.CheckSlotsAvaibleity(itemToAdd,itemToAdd.name,amountToAdd);
            Destroy(gameObject);
        }
    }
}
