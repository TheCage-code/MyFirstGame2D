using UnityEngine;

public class GameManagerTwo : MonoBehaviour
{
    public static GameManagerTwo instance;

    public int sceneIndex;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

}
