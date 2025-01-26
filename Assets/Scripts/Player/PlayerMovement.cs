using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{

    private Rigidbody2D m_Rigidbody2;
    public float speed;
    public float jumpStrength;
    private  Animator m_Animator;
   
    public BoxCollider2D boxCollider;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    private float jumpWallCoolDown;
    public float horizontalVal { get; private set; }
    private bool fall;
    public float xCorrection;
    public Transform tail;
    public GameObject[] dragonParts;
    public bool bodyEnter { get; private set;}
    public bool dead {  get; private set; }
    



    private void Awake()
    {
        m_Rigidbody2= GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();  
        boxCollider = GetComponent<BoxCollider2D>();
        dead = false;
        fall = false;
        bodyEnter=false;
        
    }
    public Animator GetAnimator() { return m_Animator; }    
    public Rigidbody2D GetRigidbody2D() { return m_Rigidbody2; }    
    private void Update()
    {


        if (dead)
        {
            if (!fall)
            {
                transform.rotation = Quaternion.Euler(transform.position.x, transform.position.y, -90);
            }

            return;
        }
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
        if (collision.gameObject.tag == "Door")
        {
            bodyEnter = true;
            if (gameObject.transform.position.x < collision.gameObject.transform.position.x)
            {
                collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            }
        }

    }
    private bool IsGrounded()
    {
        BoxCollider2D box;
        for (int i = 0; i < dragonParts.Length; i++)
        {
            box = dragonParts[i].GetComponent<BoxCollider2D>();
          RaycastHit2D raycastHit = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
            if (raycastHit.collider != null)
            {
                return true;
            }
        }
        return false;
    }
    private bool OnWall()
    {

        BoxCollider2D box;
        for (int i = 0; i < dragonParts.Length; i++)
        {
            box = dragonParts[i].GetComponent<BoxCollider2D>();
            RaycastHit2D raycastHit = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
            if (raycastHit.collider != null)
            {
                return true;
            }
        }
        return false;
    }
    public bool CanAttack()
    {
        return IsGrounded() && !OnWall() && horizontalVal == 0;
    }
    public void Die()
    {
        dead = true;
    }
    public void Fall()
    {
        fall = true;
    }
    public void Resett()
    {
        bodyEnter = false;
    }


}
