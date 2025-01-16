using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [Header("Health")]
    public float healthStart;
    public float currentHealth { get; private set; }
    private bool dead;

    [Header("iFames")]
    public float iFrameDuration;
    public float numberOfFlashes;
    private SpriteRenderer spriteRenderer;



    private void Awake()
    {
        currentHealth = healthStart;
        dead = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, healthStart);

        if (currentHealth > 0)
        {

            gameObject.GetComponent<Animator>().SetTrigger("hurt");
            StartCoroutine(InVulnerable());

        }
        else
        {
            if (!dead)
            {


                gameObject.GetComponent<Animator>().SetTrigger("die");
                dead = true;

            }
            //gameObject.GetComponent<PlayerMovement>().Die();

        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
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
    private IEnumerator InVulnerable()
    {
        Physics2D.IgnoreLayerCollision(9, 10, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlashes * 2));
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlashes * 2));

        }
        Physics2D.IgnoreLayerCollision(9, 10, false);
    }
}
