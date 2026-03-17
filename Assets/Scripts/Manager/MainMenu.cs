using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public Animator anim;

    [HideInInspector]
    public int loadingScene;
    void Start()
    {
        loadingScene = PlayerPrefs.GetInt("LastSavedScene");
    }

   
    void Update()
    {
        
    }
    public void StartGame()
    {
        PlayerPrefs.DeleteAll();
        GameData.instance.ClearData();
        SceneManager.LoadScene(1);

    }
    public void LoadGame()
    {
        SceneManager.LoadScene(loadingScene);
    }        
    
    public void QuitGame()
    {
        Application.Quit();
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowOptions()
    {
        anim.SetBool("Show",true);
    }
    public void HideOptions()
    {
        anim.SetBool("Show", false);
    }
}
