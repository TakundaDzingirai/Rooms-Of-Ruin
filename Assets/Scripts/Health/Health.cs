using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float healthStart;
    public float currentHealth { get; private set; }
    private bool dead;

    private void Awake()
    {
        currentHealth = healthStart;
        dead = false;
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, healthStart);

        if (currentHealth > 0)
        {
            gameObject.GetComponent<Animator>().SetTrigger("hurt");

        }
        else
        {
            if(!dead) {


               gameObject.GetComponent<Animator>().SetTrigger("die");
                dead = true;

            } 
                //gameObject.GetComponent<PlayerMovement>().Die();
            
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) { 
            TakeDamage(1);
        
        }
    }
    public void AddHealth(float value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, healthStart);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Heart")
        {
            AddHealth(1);
            collision.gameObject.SetActive(false);
        }
    }
}
