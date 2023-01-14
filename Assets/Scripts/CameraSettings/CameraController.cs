using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

//One Wheel Studio Code https://www.youtube.com/watch?v=3Y7TFN_DsoI&ab_channel=OneWheelStudio

public class CameraController : MonoBehaviour
{
    private CameraControlActions cameraActions;
    private InputAction movement;
    private Transform cameraTransform;

    //horizontal motion
    [SerializeField]
    private float maxSpeed = 5f;
    private float speed;
    [SerializeField]
    private float acceleration = 10f;
    [SerializeField]
    private float damping = 15f;


    //vertical motion - zooming
    //[SerializeField]
    //private float stepSize = 2f;
    //[SerializeField]
    //private float zoomDampening = 7.5f;
    //[SerializeField]
    //private float minHeight = 5f;
    //[SerializeField]
    //private float maxHeight = 25f;
    //[SerializeField]
    //private float zoomSpeed = 2f;

    //screen edge motion
    [SerializeField]
    [Range(0f, 0.1f)]
    private float edgeTolerance = 0.05f;
    [SerializeField]
    private bool useScreenEdge = true;

    //value set in various functions
    //used to update the position of the camera base object
    private Vector3 targetPosition;

    //private float zoomHeight;

    //used to track and maintain velocity w/oa rigidbody
    private Vector3 horizontalVelocity;
    private Vector3 lastPosition;

    private void Awake()
    {
        cameraActions = new CameraControlActions();
        cameraTransform = this.GetComponentInChildren<Camera>().transform;
    }
    private void OnEnable()
    {
        lastPosition = this.transform.position;
        movement = cameraActions.ActionCamera.Newaction;
        cameraActions.ActionCamera.Enable();
        //cameraActions.ActionCamera.ZoomCamera.performed += ZoomCamera;
    }

    private void OnDisable()
    {
        cameraActions.Disable();
        //cameraActions.ActionCamera.ZoomCamera.performed -= ZoomCamera;
    }

    private void Update()
    {
        GetKeyboardMovement();
        if (useScreenEdge == true)
        { CheckMouseAtScreenEdge(); }

        UpdateVelocity();
        UpdateBasePosition();
        //UpdateCameraPosition();
    }
    private void UpdateVelocity() 
    { 
        horizontalVelocity = (this.transform.position - lastPosition)/Time.deltaTime;
        horizontalVelocity.y = 0;
        lastPosition = this.transform.position;
    }
    

    private void GetKeyboardMovement() 
    {
        Vector3 inputValue = movement.ReadValue<Vector2>().x * GetCameraRight() 
            + movement.ReadValue<Vector2>().y * GetCameraUp();

        inputValue = inputValue.normalized;

        if (inputValue.sqrMagnitude > 0.1f) 
            targetPosition +=inputValue;
    }

    private Vector3 GetCameraRight()
    {
        Vector3 right = cameraTransform.right;
        right.y = 0;
        return right;
    }

    private Vector3 GetCameraUp()
    {
        Vector3 up = cameraTransform.up;
        up.z = 0;
        return up;
    }

    private void UpdateBasePosition()
    {
        if (targetPosition.sqrMagnitude > 0.1f)
        {
            speed = Mathf.Lerp(speed, maxSpeed, Time.deltaTime * acceleration);
            transform.position += targetPosition * speed * Time.deltaTime;
        }
        else
        {
            horizontalVelocity = Vector3.Lerp(horizontalVelocity, Vector3.zero, Time.deltaTime * damping);
            transform.position += horizontalVelocity * Time.deltaTime;
        }
        targetPosition = Vector3.zero;
    }
    private void CheckMouseAtScreenEdge() 
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector3 moveDirection = Vector3.zero;

            if (mousePosition.x < edgeTolerance * (Screen.width - 50))
                moveDirection += -GetCameraRight();
            else if (mousePosition.x > (1f - edgeTolerance) * (Screen.width - 50))
                moveDirection += GetCameraRight();

            if (mousePosition.y < edgeTolerance * (Screen.height - 50))
                moveDirection += -GetCameraUp();
            else if (mousePosition.y > (1f - edgeTolerance) * (Screen.height - 50))
                moveDirection += GetCameraUp();
        targetPosition += moveDirection;
    }
}


//private void ZoomCamera(InputAction.CallbackContext inputValue)
//{
//    float value = -inputValue.ReadValue<Vector2>().y / 100f;

//    if(Math.Abs(value)> 0.1f) 
//    {
//        zoomHeight = cameraTransform.localPosition.y + value * stepSize;
//        if(zoomHeight< minHeight)
//            zoomHeight = minHeight;
//        else if (zoomHeight> maxHeight)
//            zoomHeight = maxHeight; 
//    }
//}
//private void UpdateCameraPosition() 
//{
//    Vector3 zoomTarget = new Vector3(cameraTransform.localPosition.x, zoomHeight, cameraTransform.localPosition.y);
//    zoomTarget -= zoomSpeed * (zoomHeight - cameraTransform.localPosition.z) * Vector3.forward;

//    cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, zoomTarget, Time.deltaTime * zoomDampening);
//}