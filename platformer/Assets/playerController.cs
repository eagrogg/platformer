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
    }

    private void FixedUpdate()
    {
        velocity.x += acceleration * direction.x;
        velocity.x = Mathf.Clamp(velocity.x, -max_speed, max_speed);

        if(direction.x == 0)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, friction);
        }

        velocity.y -= gravity;

        velocity.y = Mathf.Max(velocity.y, -max_fall_speed);

        rb.MovePosition(rb.position + (velocity * Time.fixedDeltaTime));
    }
}
