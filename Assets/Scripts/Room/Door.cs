using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    //public Transform prevRoom;
    public Transform nextRoom;
    public CameraScript cam;
    public float centreCorrection;
    public SceneSwitch snSw;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Collision with Player detected.");
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            if (cam == null)
            {
                Debug.LogError("Cam is not assigned!");
                return;
            }

            if (collision.transform.position.x <= transform.position.x)
            {
              
                    cam.MoveToNewRoom(nextRoom);
                    Transform tail = collision.gameObject.GetComponent<PlayerMovement>().tail;
                    tail.GetComponent<BoxCollider2D>().isTrigger = false;
              

            }
          
        }
        else if(collision.tag == "Tail")
        {
            collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
           
            Debug.Log("Door Lock!!");

        }
    }


}
