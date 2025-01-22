using UnityEngine;

public class Door : MonoBehaviour
{
    //public Transform prevRoom;
    public Transform nextRoom;
    public CameraScript cam;
    public float centreCorrection;



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
                Transform tail = collision.gameObject.GetComponent<PlayerMovement>().tail;
                tail.GetComponent<BoxCollider2D>().isTrigger = false;
                
                
                

            }
          
        }
        else if(collision.tag == "Tail")
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            Debug.Log("Door Lock");

        }
    }


}
