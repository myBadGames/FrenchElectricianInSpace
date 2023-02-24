using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    public float horizontalInput;
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

    private PlayerGroundCheck groundCheck;

    public float spawnOffset;
    public GameObject background;
    public GameObject playerHolder;
    public Vector3 origin;
    public float camShiftDuration;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        groundCheck = GetComponentInChildren<PlayerGroundCheck>();
        Physics2D.gravity *= gravityMod;
        origin = GameObject.Find("BackgroundFirst").transform.position;
    }

    void Update()
    {
       // Debug.Log(transform.position.x);
        PlayerInput();

        var composer = cineCam.GetCinemachineComponent<CinemachineFramingTransposer>();

        if (inTheBack)
        {
            composer.m_DeadZoneWidth = 1;
        }
        else
        {
            if (composer.m_DeadZoneWidth > 0f)
            {
                composer.m_DeadZoneWidth -= Time.deltaTime / camShiftDuration;
            }
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            direction = horizontalInput;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Instantiate(background, GameObject.Find("BackgroundFirst").transform.position + new Vector3(spawnOffset, 0), background.transform.rotation);
            Debug.Log("Instantiate");
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            GameObject doomed = GameObject.FindGameObjectWithTag("Background");

            Destroy(doomed);
        }

        if(Input.GetKeyDown(KeyCode.U))
        {
            StartCoroutine(Move());
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
        if (collision.gameObject.CompareTag("Ground") && groundCheck.occupiedGround)
        {
            isOnGround = true;
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
            if (horizontalInput < 0)
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

    IEnumerator Move()
    {
        yield return new WaitForSeconds(Random.Range(2.5f, 5.5f));
        Debug.Log("Move");
        gameObject.transform.SetParent(playerHolder.transform);
        playerHolder.transform.position = new Vector2(0, 0);
        gameObject.transform.parent = null;
        playerHolder.GetComponent<GoBack>().GoBackToFront();
    }
}
