using UnityEngine;

public class EffectDestroyerer : MonoBehaviour
{
    public float timer;
    void Start()
    {
        Destroy(gameObject, timer);
    }

    
    void Update()
    {
        
    }
}
