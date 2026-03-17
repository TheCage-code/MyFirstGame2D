using UnityEngine;
using UnityEngine.UI;

public class CoinBank : MonoBehaviour
{
    public int bank;
    public Text bankText;

    public static CoinBank instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    void Start()
    {
        bank = PlayerPrefs.GetInt("CoinAmount", 0);
        bankText.text = "X" + bank.ToString();
    }

 
    void Update()
    {
        bankText.text = "X" + bank.ToString();
    }

    public void Money(int coinCollected)
    {
        bank += coinCollected;
        bankText.text = "X" + bank.ToString();

        DataManager.instance.CurrentCoin(bank);
        bank = PlayerPrefs.GetInt("CoinAmount");
    }



}
