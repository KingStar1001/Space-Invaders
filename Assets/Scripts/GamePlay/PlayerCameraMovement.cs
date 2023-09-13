using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraMovement : MonoBehaviour
{
    public Transform target; // Reference to the spaceship's Transform
    public Transform spaceship;
    public float rotationSpeed = 5.0f;

    private Vector3 offset;

    private void Awake()
    {
        offset = transform.position;
    }

    private void Update()
    {
        if (target == null)
            return;

        Vector3 newPosition = spaceship.position / 2f + offset;
        transform.position = newPosition;
        // Keep the camera's position fixed on the Y and Z axes
        // Vector3 desiredPosition = new Vector3(transform.position.x, target.position.y, target.position.z);

        // Smoothly interpolate the camera's position
        // transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * rotationSpeed);

        // Calculate the target rotation based on the spaceship's position
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);

        // Restrict camera rotation on the X and Y axes
        // targetRotation.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, targetRotation.eulerAngles.z);

        // Smoothly interpolate the camera's rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
