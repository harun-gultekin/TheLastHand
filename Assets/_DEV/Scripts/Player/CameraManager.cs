using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform targetTransform; // The object the camera will follow
    public Transform cameraPivot;
    private Vector3 cameraFollowVelocity = Vector3.zero;
    public GameObject player;

    public float cameraDelay = 0.1f;
    public float cameraLookSpeed = 2;
    public float cameraPivotSpeed = 2;

    public float lookAngle;
    public float pivotAngle;
    public float minimumPivotAngle = -35;
    public float maximumPivotAngle = 35;

    private bool rightclicked = false;

    private void Awake()
    {
        targetTransform = player.transform;
    }

    public void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position, ref cameraFollowVelocity, cameraDelay);
        transform.position = targetPosition;

    }

    public void RotationActivationCheck()
    {
        if (Input.GetMouseButtonDown(1))
        {
            rightclicked = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            rightclicked = false;
        }

    }

    public void RoteteCamera()
    {

        float mouseXInput = Input.GetAxis ("Mouse X");
        float mouseYInput = Input.GetAxis ("Mouse Y");
        //Debug.Log("mouseXInput:"+mouseXInput+" mouseYInput:"+mouseYInput);
        lookAngle += mouseXInput * cameraLookSpeed;
        pivotAngle -= mouseYInput * cameraPivotSpeed;
        pivotAngle = Mathf.Clamp(pivotAngle, minimumPivotAngle, maximumPivotAngle);

        Vector3 rotation = Vector3.zero;
        rotation.y = lookAngle;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;


    }

    private void LateUpdate()
    {
        RotationActivationCheck();
        FollowTarget();
        if (rightclicked) RoteteCamera();
    }
}
