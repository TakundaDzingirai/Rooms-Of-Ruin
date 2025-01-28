using UnityEngine;

public class Health : MonoBehaviour
{
    public float healthStart;
    public float currentHealth { get; private set; }
    private bool dead;

    private void Awake()
    {
        // Load health from PlayerDataManager if it exists
        if (PlayerDataManager.Instance != null)
        {
            currentHealth = PlayerDataManager.Instance.CurrentHealth;
        }
        else
        {
            currentHealth = healthStart; // Default health if no data exists
        }

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
            if (!dead)
            {
                gameObject.GetComponent<Animator>().SetTrigger("die");
                dead = true;
            }
        }

        // Save health to PlayerDataManager
        if (PlayerDataManager.Instance != null)
        {
            PlayerDataManager.Instance.CurrentHealth = currentHealth;
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

        // Save health to PlayerDataManager
        if (PlayerDataManager.Instance != null)
        {
            PlayerDataManager.Instance.CurrentHealth = currentHealth;
        }
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