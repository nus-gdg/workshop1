using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>Handles the low-level controls of the camera. For now,</para>
/// it only controls the camera's position. 
/// </summary>
public class CameraController : MonoBehaviour
{
    public float SmoothTime = 0.3f;

    public Vector3 TargetPosition
    {
        get { return targetPosition; }
        set { targetPosition = value; }
    }
    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        targetPosition.z = transform.position.z;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, SmoothTime);
    }
}
