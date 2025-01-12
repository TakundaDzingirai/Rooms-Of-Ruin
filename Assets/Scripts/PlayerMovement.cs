using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{

    private Rigidbody2D m_Rigidbody2;
    public float speed;
    public float jumpStrength;
    private  Animator m_Animator;
    private bool isGrounded=true;
    public BoxCollider2D boxCollider;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    private float jumpWallCoolDown;
    private float horizontalVal;
    



    private void Awake()
    {
        m_Rigidbody2= GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();  
        boxCollider = GetComponent<BoxCollider2D>();
    }
    public Animator GetAnimator() { return m_Animator; }    
    public Rigidbody2D GetRigidbody2D() { return m_Rigidbody2; }    
    private void Update()
    {
        horizontalVal = Input.GetAxis("Horizontal");

        if (jumpWallCoolDown > 0.2f)
        {
            m_Rigidbody2.linearVelocity = new Vector2(horizontalVal * speed, m_Rigidbody2.linearVelocity.y);

            if (OnWall() && !IsGrounded())
            {
                m_Rigidbody2.gravityScale = 0;
                m_Rigidbody2.linearVelocity = Vector2.zero;
            }
          
            else
            {
                m_Rigidbody2.gravityScale = 2.4f;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }


        }
        else
        {
            jumpWallCoolDown += Time.deltaTime;
        }
      

        //fliping code
        if (horizontalVal > 0.01f)
        {
            transform.localScale=Vector3.one;
        }
        else if (horizontalVal < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        m_Animator.SetBool("rn",horizontalVal!=0f);
        m_Animator.SetBool("grounded", IsGrounded());


        
    }
    private void Jump()
    {
        if (IsGrounded())
        {
            m_Rigidbody2.linearVelocity = new Vector2(m_Rigidbody2.linearVelocity.x, jumpStrength);
            //isGrounded = false;
            m_Animator.SetTrigger("jump");

        }
        else if (OnWall())
        {
            if (horizontalVal == 0)
            {
                m_Rigidbody2.linearVelocity= new Vector2(-Mathf.Sign(transform.localScale.x)*10,0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {

                m_Rigidbody2.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);

            }
            jumpWallCoolDown = 0;

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);

        //if ( == "moto")
        //{
        //    m_Animator.SetTrigger("die");
        //    m_Rigidbody2.angularVelocity = 0;
        //    m_Rigidbody2.gravityScale = 0;
        //    Debug.Log("Dieeeee");
          
        //}
    }
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider!=null;
    }
    private bool OnWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
    public bool CanAttack()
    {
        return IsGrounded() && !OnWall() && horizontalVal == 0;
    }

}
