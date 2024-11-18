using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // The player or object to follow
    public float smoothSpeed = 10f; // Speed of the camera following the target
    private Vector3 offset; // Offset between the camera and target

    void Start()
    {
        // Calculate the initial offset at the start
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        // Calculate the desired position based on the target's position and the offset
        Vector3 desiredPosition = target.position + offset;

        // Smoothly interpolate between the current position and the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}
