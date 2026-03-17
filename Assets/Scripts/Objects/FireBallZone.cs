using UnityEngine;

public class FireBallZone : MonoBehaviour
{

    private void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&&transform.GetComponentInParent<WizardEnemy>().isWatcher)
        {
            
            
                transform.GetComponentInParent<WizardEnemy>().Shoot();
            transform.GetComponentInParent<WizardEnemy>().isWatcher = false;
            transform.GetComponentInParent<WizardEnemy>().freqShooter = true;


        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.GetComponentInParent<WizardEnemy>().isWatcher = true;
            transform.GetComponentInParent<WizardEnemy>().freqShooter = false;
        }
    }

}
