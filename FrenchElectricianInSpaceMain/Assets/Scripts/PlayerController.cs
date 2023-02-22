using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    [SerializeField] private float horizontalInput;
    [SerializeField] private float movingSpeed;
    [SerializeField] private float velocity;
    [SerializeField] private bool jumpBool;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool isOnGround;

    [SerializeField] private CinemachineVirtualCamera cineCam;
    [SerializeField] private float deadZoneMax;
    [SerializeField] private bool inTheBack;


    [SerializeField] private float direction;

    [SerializeField] private float gravityMod;
    public float spawnOffset;
    public GameObject background;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        Physics2D.gravity *= gravityMod;
    }

    void Update()
    {
        //Debug.Log(Physics2D.gravity);
        PlayerInput();

        var composer = cineCam.GetCinemachineComponent<CinemachineFramingTransposer>();

        if (inTheBack)
        {
             composer.m_DeadZoneWidth = 1;
        }
        else
        {
            composer.m_DeadZoneWidth = 0.1f;
        }


        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            direction = horizontalInput;
        }


        if(Input.GetKeyDown(KeyCode.P))
        {
            Instantiate(background, transform.position + new Vector3(spawnOffset,0), background.transform.rotation);
            Debug.Log("Instantiate");
        }
    }

    private void FixedUpdate()
    {
        PhysicsDependentMovement();

        velocity = playerRb.velocity.magnitude;

    }

    void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBool = true;
        }
    }

    void PhysicsDependentMovement()
    {
        if (isOnGround)
        {
            playerRb.velocity = new Vector2(horizontalInput * movingSpeed, playerRb.velocity.y);
        }


        if (jumpBool && isOnGround)
        {
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpBool = false;
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            if(!isOnGround)
            {
                isOnGround = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BackHalf"))
        {
            if(horizontalInput < 0)
            {
                inTheBack = true;
            }
            else
            {
                inTheBack = false;
            }
        }
    }

    void Die()
    {
        Debug.Log("Death");
    }
}
