using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField]
    private float jumpForce, maxSpeed, accelerationForce = 5;

    [SerializeField]
    private Rigidbody2D rb2D;

    [SerializeField]
    private Collider2D groundDetectTrigger;

    [SerializeField]
    private ContactFilter2D groundContactFilter;

    [SerializeField]
    private Collider2D PlayerGroundCollider; 

    [SerializeField]
    private Text countText;

    [SerializeField]
    private Text Progress;

    [SerializeField]
    private PhysicsMaterial2D playerMovingPhysicsMaterial, playerStoppingPhysicsMaterial;

    [SerializeField]
    private float startTimeBtwAttack;

    [SerializeField]
    private float attackRange;

    [SerializeField]
    private Transform attackPos;

    [SerializeField]
    private LayerMask whatIsEnemy;

    [SerializeField]
    private int Damage;

    private bool facingRight = true;
    private bool isOnGround;
    private float horizontalInput;
    private float timeBtwAttack;
    private Collider2D[] groundhitDetectionResults = new Collider2D[20];
    private CheckPoint currentCheckPoint;
    private int count = 0;

    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        SetCountText();
    }
    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Ground", false);
        UpdateIsOnGround();
        UpdateHorizontalInput();
        HandleJumpInput();
        Attack();

    }

    private void FixedUpdate()
    {
        Move();

        Direction();

        UpdatePhysicsMaterial();

    }

    private void UpdatePhysicsMaterial()
    {
        if (Mathf.Abs(horizontalInput) > 0)
        {
            //TODO moving material
            PlayerGroundCollider.sharedMaterial = playerMovingPhysicsMaterial;
        }
        else
        {
            //Stop physics material
            PlayerGroundCollider.sharedMaterial = playerStoppingPhysicsMaterial;
        }
    }

    private void HandleJumpInput()
    {

        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
           
        }
    }
    private void UpdateIsOnGround()
    {
        isOnGround = groundDetectTrigger.OverlapCollider(groundContactFilter, groundhitDetectionResults) > 0;
        Debug.Log(isOnGround);

        if (isOnGround == true)
        {
            anim.SetBool("Ground", true);
        }
        else
        {
            anim.SetBool("Ground", false);
        }

    }
    private void UpdateHorizontalInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

    }
 
    private void Move()
    {

            rb2D.AddForce(Vector2.right * horizontalInput * accelerationForce);
            Vector2 clampedVelocity = rb2D.velocity;
            clampedVelocity.x = Mathf.Clamp(rb2D.velocity.x, -maxSpeed, maxSpeed);
            rb2D.velocity = clampedVelocity;

            
    }
   private void Direction()
   {
        UpdateAnimationParameters();
        if (horizontalInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && facingRight)
        {
            Flip();
        }
    }

    public void Respawn()
    {
        if(currentCheckPoint == null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Debug.Log("Respawn triggered." );
        }
        else
        {
            rb2D.velocity = Vector2.zero;
            transform.position = currentCheckPoint.transform.position;
        }
        
    }

    public void SetCurrentCheckPoint(CheckPoint newCurrentCheckpoint)
    {
        currentCheckPoint = newCurrentCheckpoint;
        Debug.Log("Checkpoint reached." + currentCheckPoint);
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            Progress.text = "Get to the Gate! You have all of the, crystals!";
        }
    }
    void Attack()
    {
        if (timeBtwAttack <= 0)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                Collider2D[] enemyToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
                for (int i = 0; i < enemyToDamage.Length; i++)
                {
                    enemyToDamage[i].GetComponent<Enemy>().TakeDamage(Damage);
                   
                }
                anim.SetTrigger("attack");
            }
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
    void UpdateAnimationParameters()
    {
        anim.SetFloat("VSpeed", rb2D.velocity.y);
        anim.SetBool("Ground", isOnGround);
        anim.SetFloat("Speed", Mathf.Abs(horizontalInput));
        
    }

}    