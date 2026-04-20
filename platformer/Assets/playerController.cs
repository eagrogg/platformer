using UnityEngine;

public class playerController : MonoBehaviour
{
    private Vector2 direction = Vector2.zero;
    private Rigidbody2D rb;
    
    public float max_speed = 5f;
    private Vector2 velocity;
    public float acceleration = 1.5f;
    public float friction = 0.75f;
    
    public float gravity = 0.5f;
    public float max_fall_speed = 9.8f;

    public LayerMask mask;
    private RaycastHit2D groundCheckL;
    private RaycastHit2D groundCheckR;

    private bool jumpCheck;
    public float jumpForce = 15f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            direction.x = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            direction.x = -1;
        }
        else
        {
            direction.x = 0;
        }

        if (Input.GetKey(KeyCode.W))
        {
            direction.y = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction.y = -1;
        }
        else
        {
            direction.y = 0;
        }

        direction = direction.normalized;

        if (Input.GetKey(KeyCode.Space))
        {
            jumpCheck = true;
        }
        else
        {
            jumpCheck = false;
        }
    }

    private void FixedUpdate()
    {
        velocity.x += acceleration * direction.x;
        velocity.x = Mathf.Clamp(velocity.x, -max_speed, max_speed);

        if(direction.x == 0)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, friction);
        }

        bool isGrounded = false;
        if(velocity.y < 0)
        {
            groundCheckL = Physics2D.Raycast(rb.position + new Vector2(-0.5f, 0), Vector2.down, 1, mask);
            groundCheckR = Physics2D.Raycast(rb.position + new Vector2(0.5f, 0), Vector2.down, 1, mask);
            isGrounded = (groundCheckL || groundCheckR);
        }

        
        float groundOffset = 0;

        if (isGrounded)
        {
            groundOffset = Mathf.Max(groundCheckL.distance, groundCheckR.distance) - 0.5f;
            velocity.y = 0;

            if (jumpCheck)
            {
                velocity.y = jumpForce;
                isGrounded = false;
            }
        }
        else
        {
            velocity.y -= gravity;

            velocity.y = Mathf.Max(velocity.y, -max_fall_speed);
        }

        rb.MovePosition(rb.position + (velocity * Time.fixedDeltaTime) - new Vector2(0,groundOffset));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(GetComponent<Rigidbody2D>().position + new Vector2(-0.5f, 0f), Vector2.down);
        Gizmos.DrawRay(GetComponent<Rigidbody2D>().position + new Vector2(0.5f, 0f), Vector2.down);
    }
}
