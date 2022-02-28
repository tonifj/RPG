using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCamera : MonoBehaviour
{
    Camera cam;

    Transform target;
    float smoothSpeed = 0.0325f/2f;
    Vector3 offset;
    Vector3 desiredPosition;
    Vector3 smoothedPosition;
    void Start()
    {
        target = new GameObject().transform;
    }

    // Update is called once per frame
    void Update()
    {
        BattleCameraMovement();
    }

    void BattleCameraMovement()
    {
        desiredPosition = target.position + offset;
        desiredPosition.y += 10;
        desiredPosition.z -= 5;
        smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
    public void SetTarget(Transform new_target)
    {
        target = new_target;
    }
}
