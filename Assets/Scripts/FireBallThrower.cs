using UnityEngine;
using UnityEngine.UIElements;

public class FireBallThrower : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float direction = 1;
    public float speed;
    private bool hit;
    private Animator m_Animator;
    private float lifetime;
    public BoxCollider2D boxCollider;
    public PlayerMovement player;

    // Update is called once per frame
    private void Awake()
    {
        
        m_Animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {

        if (hit||player.dead)
        {
            return;
        }
        float move = direction * speed * Time.deltaTime;
        transform.Translate(move,0,0);
        lifetime += Time.deltaTime;
        if (lifetime > 5)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        m_Animator.SetTrigger("explode");
       
    }
    public void SetDirection(float dir)
    {
        lifetime = 0;
        direction = dir;
        gameObject.SetActive(true);
        boxCollider.enabled = true;
        hit = false;

        float localScaleX=transform.localScale.x;
        if (Mathf.Sign(localScaleX) != dir)
        {
            localScaleX = -localScaleX;
        }
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);

    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
