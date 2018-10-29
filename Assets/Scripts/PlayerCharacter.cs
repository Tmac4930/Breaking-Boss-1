using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private bool isOnGround;
    private float horizontalInput;
    private Collider2D[] groundhitDetectionResults = new Collider2D[20];
    private CheckPoint currentCheckPoint;

    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        UpdateIsOnGround();
        UpdateHorizontalInput();
        HandleJumpInput();
    }

    private void FixedUpdate()
    {
        Move();
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
        
    }
    private void UpdateHorizontalInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }
 
    private void Move()
    {     
            rb2D.AddForce(Vector2.right * horizontalInput * accelerationForce);
            Vector2 clampedVelocity = rb2D.velocity;
            clampedVelocity.x = Mathf.Clamp(rb2D.velocity.x, -maxSpeed, maxSpeed);
            rb2D.velocity = clampedVelocity;
    }

    public void Respawn()
    {
        if(currentCheckPoint == null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
    }


}    