using UnityEngine;
using TMPro;


public class StoreItems : MonoBehaviour
{
   // public string itemName;
    public int itemSellPrice;
    public int itemBuyPrice;
    public GameObject itemToAdd;
    public int amountToAdd;
    GameManagerTwo gameManager;
    Inventory inventory;
    TextMeshProUGUI buyPriceText;
    public ShopNpc shopNpc;


    void Start()
    {
        gameManager = GameManagerTwo.instance;
        inventory = gameManager.GetComponent<Inventory>();

        buyPriceText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        buyPriceText.text=itemBuyPrice.ToString();  
        shopNpc=transform.root.GetComponent<ShopNpc>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
    }

    public void BuyItems()
    {
        if (!shopNpc.sellItems)
        {
            if (itemBuyPrice <= CoinBank.instance.bank)
            {
                CoinBank.instance.Money(-itemBuyPrice);
                inventory.CheckSlotsAvaibleity(itemToAdd, itemToAdd.name, amountToAdd);
                buyPriceText.text = itemBuyPrice.ToString();
            }
        }
        else if(inventory.inventoryItems.ContainsKey(itemToAdd.name)) 
        {
            inventory.UseInventoryItems(itemToAdd.name);
            CoinBank.instance.Money(itemSellPrice);
            buyPriceText.text=itemSellPrice.ToString();

        }
        
    }
    public void UpdateText()
    {
        if (!shopNpc.sellItems)
        {
            buyPriceText.text = itemBuyPrice.ToString();
        }
        else
        {
            buyPriceText.text = itemSellPrice.ToString();
        }
    }
}
