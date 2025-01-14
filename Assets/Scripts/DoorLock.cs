using UnityEngine;

public class DoorLock : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
