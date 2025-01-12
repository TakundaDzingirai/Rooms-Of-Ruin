using UnityEngine;

public class Moto : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public PlayerMovement playerMovement;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerMovement.GetAnimator().SetTrigger("die");
        playerMovement.GetRigidbody2D().gravityScale = 0;
        playerMovement.GetRigidbody2D().linearVelocity = Vector2.zero;

    }
}
