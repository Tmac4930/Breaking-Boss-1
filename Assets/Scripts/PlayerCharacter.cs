﻿using System.Collections;
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

    Animator anim;
    bool facingRight = true;


    private bool isOnGround;
    private float horizontalInput;
    private Collider2D[] groundhitDetectionResults = new Collider2D[20];
    private CheckPoint currentCheckPoint;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Ground", false);
        UpdateIsOnGround();
        UpdateHorizontalInput();
        HandleJumpInput();
    }

    private void FixedUpdate()
    {
        Move();
        Direction();
        
    }

    private void HandleJumpInput()
    {

        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.Setbool("Vspeed");
        }
    }
    private void UpdateIsOnGround()
    {
        isOnGround = groundDetectTrigger.OverlapCollider(groundContactFilter, groundhitDetectionResults) > 0;
        anim.SetBool("Ground", isOnGround);

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

            anim.SetFloat("Speed", Mathf.Abs("Horizontal"));
    }
    private void Direction()
    {
        if (Move > 0 && !facingRight)
            Flip();
        else if (Move < 0 && facingRight)
            Flip();
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
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}    