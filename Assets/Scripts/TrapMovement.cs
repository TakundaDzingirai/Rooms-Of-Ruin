using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class TrapMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float damage;
    public float speed;
    private Animator animator;
    public float dir { get; private set; }

    private void Awake()
    {
        dir = -1;
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        float move = speed * dir *Time.deltaTime; 
        transform.Translate(move, 0, 0);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
        else if (collision.gameObject.tag == "Left")
        {
            ChangeDirection();
            animator.SetBool ("saw",false);

        }
        else if(collision.gameObject.tag == "Right")
        {
            ChangeDirection();
            animator.SetBool("saw",true);
        }
    }
    public void ChangeDirection()
    {
        dir =-dir;
    }
}
