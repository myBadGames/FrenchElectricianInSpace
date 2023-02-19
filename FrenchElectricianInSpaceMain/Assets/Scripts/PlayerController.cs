using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    [SerializeField] private float horizontalInput;
    [SerializeField] private float movingSpeed;
    [SerializeField] private bool jumpBool;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool isOnGround;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
   
    }

    void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        PhysicsDependentMovement();
    }

    void PlayerInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if(Input.GetKeyDown(KeyCode.Space))
        {
            jumpBool = true;
        }
    }

    void PhysicsDependentMovement()
    {
        if(isOnGround)
        {
            playerRb.AddForce(Vector2.right * movingSpeed * horizontalInput, ForceMode2D.Force);
        }

        if (jumpBool && isOnGround)
        {
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            jumpBool = false;
        }
        else if(collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = false;
        }
    }

    void Die()
    {
        Debug.Log("Death");
    }
}
