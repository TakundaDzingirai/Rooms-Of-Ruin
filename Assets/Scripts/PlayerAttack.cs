using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator m_Animator;
    private float attackCoolDown=Mathf.Infinity;
    public float attackCoolUp;
    private PlayerMovement m_PlayerMovement;
    public Transform firePoint;
    public GameObject[] fireballs;
    private void Awake()
    {
        
        m_Animator = GetComponent<Animator>();
        m_PlayerMovement= GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) &&attackCoolDown > attackCoolUp&&m_PlayerMovement.CanAttack())
        {
            Attack();
        }
        else
        {
            attackCoolDown += Time.deltaTime;
        }
        
    }
    private void Attack()
    {
        attackCoolDown = 0;
        m_Animator.SetTrigger("attack");
      
        fireballs[FindFireBall()].transform.position = firePoint.position;
        fireballs[FindFireBall()].GetComponent<FireBallThrower>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
    public int FindFireBall()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {

               
                return i;
            }
        }
        return 0;
    }
}
