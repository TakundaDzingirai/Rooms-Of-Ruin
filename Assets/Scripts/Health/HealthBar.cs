
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image totalHealthbar;
    public Image healthbar;
    public Health health;
    private void Start()
    {
        totalHealthbar.fillAmount = health.currentHealth/10;
    }

    // Update is called once per frame
    void Update()
    {
        float val =health.currentHealth / 10;
        //print(val);
        healthbar.fillAmount = val;
    }
}
