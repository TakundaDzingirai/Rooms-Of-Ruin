using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float speed; // Speed of camera movement
    public float centreCorrection; // Correction to center the camera in the new room
    public Transform player; // Reference to the player's transform

    private float currentPosX; // Current target X position for the camera
    private float originalYPos; // Original Y position of the camera
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        // Store the original Y position of the camera
        originalYPos = transform.position.y;
    }

    private void Update()
    {
        if (player != null)
        {
            // Calculate the target Y position based on the player's vertical position
            float targetYPos = originalYPos; // Default to the original Y position
            if (player.position.y > 4)
            {
                // If the player's Y position is greater than 4, move the camera slightly up
                targetYPos = originalYPos + (player.position.y - 4);
            }

            // Smoothly interpolate the camera's position
            Vector3 targetPosition = new Vector3(currentPosX, targetYPos, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, speed);
        }
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        if (_newRoom != null)
        {
            // Update the target X position for the camera
            currentPosX = _newRoom.position.x + centreCorrection;

            // Reset the original Y position to the current Y position when moving to a new room
            originalYPos = transform.position.y;
        }
    }
}