using UnityEngine;

public class TrapMovement : MonoBehaviour
{
    public float damage;
    public float speed;
    private Animator animator;
    public float dir { get; private set; }
    public float sawDist;
    private float leftEdge;
    private float rightEdge;
    private Rigidbody2D rb;

    private void Awake()
    {
        dir = -1;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Calculate movement boundaries
        leftEdge = transform.position.x - sawDist;
        rightEdge = transform.position.x + sawDist;
    }

    private void Update()
    {
        // Prevent trap from moving beyond boundaries
        if (transform.position.x <= leftEdge)
        {
            transform.position = new Vector3(leftEdge, transform.position.y, transform.position.z); // Snap to edge
            ChangeDirection();
            if (animator != null) animator.SetBool("saw", false);
        }
        else if (transform.position.x >= rightEdge)
        {
            transform.position = new Vector3(rightEdge, transform.position.y, transform.position.z); // Snap to edge
            ChangeDirection();
            if (animator != null) animator.SetBool("saw", true);
        }

        // Apply velocity for consistent movement
        rb.linearVelocity = Vector2.right * speed * dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Health health = collision.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }

    private void ChangeDirection()
    {
        dir = -dir; // Flip direction
    }
}
