using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Experience : MonoBehaviour
{
    public Image expImg;
    [HideInInspector]
    public float currentExp;
    public float expToNextLevel;
    public static Experience instance;
    public Text levelText;
    public int currentLevel;
    public AudioSource levelUpAS;


    private void Awake()
    {
       
        if (instance == null) 
        {  instance = this; }
    }

    void Start()
    {
        expImg.fillAmount = currentExp / expToNextLevel;
        currentLevel = 1;
        levelText.text = currentLevel.ToString();
        currentExp = PlayerPrefs.GetFloat("Experience", 0);
        expToNextLevel = PlayerPrefs.GetFloat("ExperienceTNL", expToNextLevel);
        currentLevel = PlayerPrefs.GetInt("CurrentLevel",1);
    }

   
    void Update()
    {
        levelText.text = currentLevel.ToString();
        expImg.fillAmount = currentExp / expToNextLevel;
    }
    public void expMod(float experience)
    {
        
        currentExp += experience;

        
        expToNextLevel = PlayerPrefs.GetFloat("ExperienceTNL", expToNextLevel);




        expImg.fillAmount = currentExp / expToNextLevel;
        if(currentExp >= expToNextLevel)
        {
            expToNextLevel *= 2;
            currentExp = 0;
            currentLevel++;
            levelText.text = currentLevel.ToString();
            PlayerHealth.instance.maxHealth += 5;
            PlayerHealth.instance.currentHealth += 5;
           



            AudioManager.Instance.PlayAudio(levelUpAS);




            

           // currentLevel = PlayerPrefs.GetInt("CurrentLevel",currentLevel);
        }

        
    }

    public void DataSave()
    {
        DataManager.instance.ExperienceData(currentExp);
        DataManager.instance.ExperienceToNextLevel(expToNextLevel);
        DataManager.instance.LevelData(currentLevel);

        currentExp = PlayerPrefs.GetFloat("Experience");
        expToNextLevel = PlayerPrefs.GetFloat("ExperienceTNL");
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");

        DataManager.instance.CurrentHealth(PlayerHealth.instance.maxHealth);
        PlayerHealth.instance.currentHealth = PlayerPrefs.GetFloat("CurrentHealth");
        DataManager.instance.MaxHealth(PlayerHealth.instance.maxHealth);
        PlayerHealth.instance.maxHealth = PlayerPrefs.GetFloat("MaxHealth");

        GameData.instance.ClearAllDataList();
        GameManagerTwo.instance.GetComponent<Inventory>().InventoryToData();
        GameData.instance.Save();

        DataManager.instance.LastSavedScene(SceneManager.GetActiveScene().buildIndex);
        GameManagerTwo.instance.sceneIndex = PlayerPrefs.GetInt("LastSavedScene");
        
        
    }
    
}
