using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float rotationSpeed = 20.0f;
    public Vector3 rotationAxis = Vector3.up;

    private void Update()
    {
        Quaternion targetRotation = Quaternion.identity;

        targetRotation = Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, rotationAxis) * transform.rotation;

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime);
    }
}
