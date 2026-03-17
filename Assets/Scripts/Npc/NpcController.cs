using UnityEngine;

public class NpcController : MonoBehaviour
{
    public string[] dialogue;
    public string nameOfNpc;
    private bool hasTalked = false;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasTalked)
        {
            hasTalked = true;
            DialogueSystem.instance.AddDialogue(dialogue,nameOfNpc);
            DialogueSystem.instance.CreateDialogue();

        }
    }
}
