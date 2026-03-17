using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public Image healthBar;

    bool isImmune;
    public float immunityTime;

    Animator anim;
    public static PlayerHealth instance;

    private void Awake()
    {
        if (instance == null) { instance = this; }
    }

    void Start()
    {
        currentHealth = maxHealth;
        maxHealth = PlayerPrefs.GetFloat("MaxHealth", maxHealth);
        currentHealth=PlayerPrefs.GetFloat("CurrentHealth",currentHealth);
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.fillAmount = currentHealth / maxHealth;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")&& !isImmune)
        {
            currentHealth -= collision.GetComponent<BaseEnemy>().damage;
            
            StartCoroutine(Immunity());
            anim.SetTrigger("TakeHit");

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                anim.SetBool("Death",true);
                Destroy(gameObject);
            }
        }
        if (collision.CompareTag("TriggerEnemyWeapon") && !isImmune)
        {
            currentHealth -= collision.GetComponentInParent<Enemy>().damage;
            StartCoroutine(Immunity());
            anim.SetTrigger("TakeHit");
        }
        if (currentHealth < 0)
        {
            currentHealth = 0;
            Destroy(gameObject);
        }
    }

   

    IEnumerator Immunity()
    {
        isImmune = true;
        yield return new WaitForSeconds(immunityTime);
        isImmune = false;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
            Destroy(gameObject);
            SceneManager.LoadScene("MainMenu");
        }
    }
}
