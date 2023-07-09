using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        // The target object to follow
    public float smoothSpeed = 0.5f; // The smoothness of camera movement

    private Vector3 offset;         // The initial offset between the camera and the target

    private void Start()
    {
        // Calculate the initial offset
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        // Calculate the desired position based on the target's position and the offset
        Vector3 desiredPosition = target.position + offset;

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update the camera's position
        transform.position = smoothedPosition;
    }
}