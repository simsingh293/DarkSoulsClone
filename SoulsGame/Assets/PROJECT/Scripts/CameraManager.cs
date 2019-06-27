using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public bool lockon;

    public float followSpeed = 9;
    public float mouseSpeed = 2;
    public float controllerSpeed = 7;

    public Transform target;

    public Transform pivot;
    public Transform CameraTransform;

    public float turnSmoothing = 0.1f;
    public float minAngle = -15;
    public float maxAngle = 35;

    float smoothX;
    float smoothY;
    float smoothXVelocity;
    float smoothYVelocity;
    public float lookAngle;
    public float tiltAngle;

    public static CameraManager singleton;

    private void Awake()
    {
        if(singleton != null)
        {
            Destroy(this);
        }
        singleton = this;
    }

    public void Init(Transform t)
    {
        target = t;

        CameraTransform = Camera.main.transform;
        pivot = CameraTransform.parent;
    }

    public void Tick(float d)
    {
        // Mouse Input
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        // Controller Input
        float c_H = Input.GetAxis("RightJoystick X");
        float c_V = Input.GetAxis("RightJoystick Y");

        float targetSpeed = mouseSpeed;

        if(c_H != 0 || c_V != 0)
        {
            h = c_H;
            v = c_V;
            targetSpeed = controllerSpeed;
        }

        FollowTarget(d);
        HandleRotations(d, v, h, targetSpeed);
    }

    void FollowTarget(float d)
    {
        float speed = d * followSpeed;
        Vector3 targetPosition = Vector3.Lerp(transform.position, target.position, speed);
        transform.position = targetPosition;
    }

    void HandleRotations(float d, float v, float h, float targetSpeed)
    {
        if(turnSmoothing > 0)
        {
            smoothX = Mathf.SmoothDamp(smoothX, h, ref smoothXVelocity, turnSmoothing);
            smoothY = Mathf.SmoothDamp(smoothY, v, ref smoothYVelocity, turnSmoothing);
        }
        else
        {
            smoothX = h;
            smoothY = v;
        }

        if (lockon)
        {

        }


        lookAngle += smoothX * targetSpeed;
        transform.rotation = Quaternion.Euler(0, lookAngle, 0);

        tiltAngle -= smoothY * targetSpeed;
        tiltAngle = Mathf.Clamp(tiltAngle, minAngle, maxAngle);
        pivot.localRotation = Quaternion.Euler(tiltAngle, 0, 0);
    }

}
