using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform prevRoom;
    public Transform nextRoom;
    public CameraScript cam;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Collision with Player detected.");
            if (cam == null)
            {
                Debug.LogError("Cam is not assigned!");
                return;
            }

            if (collision.transform.position.x < transform.position.x)
            {
                Debug.Log("Moving to Next Room");
                cam.MoveToNewRoom(nextRoom);
            }
            else
            {
                Debug.Log("Moving to Previous Room");
                cam.MoveToNewRoom(prevRoom);
            }
        }
    }


}
