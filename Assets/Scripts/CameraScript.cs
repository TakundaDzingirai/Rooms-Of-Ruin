using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;
    public float centreCorrection;
    private void Update()
    {
        // Smoothly interpolate the camera position
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z),
            ref velocity, speed);

      

    }


    public void MoveToNewRoom(Transform _newRoom)
    {
        // Update the target position
        currentPosX = _newRoom.position.x+centreCorrection;

        
    }



}
