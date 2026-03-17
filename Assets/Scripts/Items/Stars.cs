using UnityEngine;

public class Stars : MonoBehaviour
{
    public int starAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TriggerZone"))
        {
            StarBank.instance.Collect(starAmount);
            Destroy(gameObject);
        }
    }
}
