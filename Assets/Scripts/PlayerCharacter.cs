using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField]
    private float jumpForce, maxSpeed, accelerationForce = 5;

    [SerializeField]
    private Rigidbody2D rb2D;

    private bool isOnGround;
    private float horizontalInput;
    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
       
        if (Input.GetButtonDown("Jump"))
        {
            rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {     
            rb2D.AddForce(Vector2.right * horizontalInput * accelerationForce);
            Vector2 clampedVelocity = rb2D.velocity;
            clampedVelocity.x = Mathf.Clamp(rb2D.velocity.x, -maxSpeed, maxSpeed);
            rb2D.velocity = clampedVelocity;
    }
}    