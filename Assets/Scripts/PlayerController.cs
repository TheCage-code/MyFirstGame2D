using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;    
    private Rigidbody2D rb2D;
    private Animator anim;
    private float moveDirection;
    private bool facingright;
    private bool isGrounded;
    public LayerMask groundLayer;
    public float groundCheckRadius;
    public float attackRate = 2f;
    float nextAttack = 0;
    

    //[SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float jumpSpeed = 2f;
    public GameObject groundCheck;

    public Transform attackPoint;
    public float attackDistance;
    public LayerMask enemyLayer;
    //public float damage;
    public GameObject ninjaStar;
    public Transform firePoint;
    public AudioSource swordAS;

    public GameObject inventory;
    bool invIsActive=false;

    WeaponStat weaponStat;

    public GameObject playerPanel;
    bool panelIsActive;

    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        weaponStat = GetComponent<WeaponStat>();    
    }


    void Update()
    {
        CheckAnimations();
        Jump();
        Shoot();


        if (Time.time > nextAttack)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                nextAttack = Time.time + 1f / attackRate;
            }
        }
        if (Input.GetKeyDown(KeyCode.I) && !invIsActive)
        {
            inventory.SetActive(true);
            invIsActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.I) && invIsActive)
        {
            inventory.SetActive(false);
            invIsActive = false;
        }
        if (Input.GetKeyDown(KeyCode.C)&&!panelIsActive)
        {
            playerPanel.SetActive(true);
            panelIsActive = true;
        }
        else if(Input.GetKeyDown(KeyCode.C)&&panelIsActive)
        {
            playerPanel.SetActive(false) ;
            panelIsActive = false;
        }

    }
    private void FixedUpdate()
    {
        Move();
        Run();

       
    }


    private void Move()
    {
        moveDirection = Input.GetAxisRaw("Horizontal");
        rb2D.linearVelocity = new Vector2(moveDirection * PlayerStats.instance.runSpeed, rb2D.linearVelocity.y);

    }

    public void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (StarBank.instance.bankStar > 0)
            {
            Instantiate(ninjaStar, firePoint.position, firePoint.rotation);
                StarBank.instance.bankStar -= 1;

                PlayerPrefs.SetInt("StarAmount",StarBank.instance.bankStar);
            }
        }
       

    }


    public void Attack()
    {
        float numb = Random.Range(0, 2);
        if (numb == 0 ) 
        {
          anim.SetTrigger("Attack1");
            AudioManager.Instance.PlayAudio(swordAS);
        }
        if (numb == 1)
        {
            anim.SetTrigger("Attack2");
        }

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackDistance,enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<BaseEnemy>().TakeDamage(weaponStat.DamageInput());
        }

        
    }
    
    

   

    private void Run()
    {
        if (moveDirection > 0)
        {
            anim.SetBool("Run",true);
        }
        if (moveDirection == 0)
        {
            anim.SetBool("Run", false);
        }
        if (moveDirection < 0)
        {
            anim.SetBool("Run", true);
        }
        if (moveDirection > 0 && facingright)
        {
            CharacterFlip();
        }
        if (moveDirection < 0 && !facingright)
        {
            CharacterFlip();
        }
    }
    private void CharacterFlip()
    {
        facingright = !facingright;
        Vector3 thescale = transform.localScale;
        thescale.x *= -1;
        transform.localScale = thescale;
    }

    private void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRadius, groundLayer);
        if (Input.GetKey(KeyCode.Space)&& isGrounded)
        {
            
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, jumpSpeed);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.transform.position, groundCheckRadius);
        Gizmos.DrawWireSphere(attackPoint.position,attackDistance); 
    }

    void CheckAnimations()
    {
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity",rb2D.linearVelocity.y);
    }

}
