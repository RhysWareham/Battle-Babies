using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    // Public variables exposed to the Unity menus.
    public float maxSpeed;
    public float jumpForce;
    private float moveInput;
    private float x;
    private float y;

    [SerializeField]
    private string controllerPrefix = "";

    private Rigidbody2D rb;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject win;


    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpValue;

    private void Start()
    {
        extraJumps = extraJumpValue;
        rb = GetComponent<Rigidbody2D>();
        //rb.velocity = new Vector2(0, 0);
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        moveInput = Input.GetAxis("HorizontalP1");
        Debug.Log(moveInput);
        rb.velocity = new Vector2(moveInput * maxSpeed, rb.velocity.y);

        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
    }

    void Update()
    {
        if (isGrounded == true)
        {
            extraJumps = extraJumpValue;
        }
        if (Input.GetButtonDown("AButtonP1") && extraJumps > 0 || Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if (Input.GetButtonDown("AButtonP1") && extraJumps == 0 && isGrounded == true || Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        if (player.transform.position.y < -10)
        {
            Destroy(player);
        }

        if (GameObject.FindGameObjectsWithTag("Player").Length == 1)
        {
            win.SetActive(true);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0f);
        
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Projectile" && rb.)
    //}

}