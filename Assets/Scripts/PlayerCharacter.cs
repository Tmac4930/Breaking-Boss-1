using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField]
    private float jump, maxSpeed, accelerationForce = 5;

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
       
    }

private void FixedUpdate()
    {
        rb2D.AddForce(Vector2.right * horizontalInput * accelerationForce);
        Vector2 clampedVolocity = rb2D.velocity;
        clampedVolocity.x = Mathf.Clamp(rb2D.velocity.x, -maxSpeed, maxSpeed);
    }
}    