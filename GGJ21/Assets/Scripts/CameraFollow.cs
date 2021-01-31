using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = .5f;

    public Vector3 offset;

    private void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
