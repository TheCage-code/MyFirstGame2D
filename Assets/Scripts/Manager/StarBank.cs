using UnityEngine;
using UnityEngine.UI;

public class StarBank : MonoBehaviour
{
    public int bankStar;
    public static StarBank instance;
    public Text starText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }
    void Start()
    {
        bankStar = PlayerPrefs.GetInt("StarAmount", 0);
        starText.text = "X" + bankStar.ToString();
    }

   
    void Update()
    {
        starText.text = "X" + bankStar.ToString();
    }

    public void Collect(int starCollected)
    {
        bankStar += starCollected;
        starText.text = "X" + bankStar.ToString();

        DataManager.instance.CurrentStars(bankStar);
        bankStar = PlayerPrefs.GetInt("StarAmount");
    }
}
