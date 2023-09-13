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

#if UNITY_WEBGL
        targetRotation = Quaternion.AngleAxis(rotationSpeed / 10f * Time.deltaTime, rotationAxis) * transform.rotation;
#else
        targetRotation =  Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, rotationAxis) * transform.rotation;
#endif
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime);
    }
}
