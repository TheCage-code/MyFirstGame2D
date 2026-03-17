using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButtons : MonoBehaviour
{
    GameManagerTwo gameManager;
    Inventory inventory;
    void Start()
    {
        gameManager = GameManagerTwo.instance;
        inventory = gameManager.GetComponent<Inventory>();
    }

   public void UseItem()
    {
        inventory.EquipmentInInventory(GetComponent<ItemUse>().itemType);
        if (GetComponent<ItemUse>().itemType != ItemType.USABLE)
        {
            if (transform.GetChild(1).gameObject.activeSelf)
            {
                transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                transform.GetChild(1).gameObject.SetActive(true);
                EquipmentController.instance.UpdateImage(GetComponent<Image>().sprite.texture);
            }
        }
        else
        {
            inventory.UseInventoryItems(gameObject.name);
        }

    }
}
