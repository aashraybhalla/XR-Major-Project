using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WheelchairController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform wheelchairBody;
    [SerializeField] private XRGrabInteractable leftHandle;
    [SerializeField] private XRGrabInteractable rightHandle;
    [SerializeField] private Transform xrOrigin;
    
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float smoothing = 5f;
    
    private bool isLeftHandleGrabbed;
    private bool isRightHandleGrabbed;
    private Vector3 previousXRPosition;
    private float currentSpeed;
    private float targetSpeed;
    private Vector3 moveDirection;

    private void Start()
    {
        // Set up grab events for both handles
        leftHandle.selectEntered.AddListener(OnLeftHandleGrabbed);
        leftHandle.selectExited.AddListener(OnLeftHandleReleased);
        rightHandle.selectEntered.AddListener(OnRightHandleGrabbed);
        rightHandle.selectExited.AddListener(OnRightHandleReleased);
        
        previousXRPosition = xrOrigin.position;
    }

    private void Update()
    {
        if (!isLeftHandleGrabbed && !isRightHandleGrabbed)
        {
            // Gradually slow down when not being pushed
            targetSpeed = 0f;
            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * smoothing);
            return;
        }

        // Calculate XR Origin movement direction
        Vector3 xrMovement = xrOrigin.position - previousXRPosition;
        xrMovement.y = 0; // Remove vertical movement

        if (xrMovement.magnitude > 0.001f) // Check if there's significant movement
        {
            moveDirection = xrMovement.normalized;
            
            // Calculate speed based on XR movement magnitude
            targetSpeed = xrMovement.magnitude * moveSpeed;
            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * smoothing);

            // Move the wheelchair
            transform.position += moveDirection * currentSpeed * Time.deltaTime;

            // Update wheelchair rotation to face movement direction
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smoothing);
        }

        // Update previous position
        previousXRPosition = xrOrigin.position;
    }

    private void OnLeftHandleGrabbed(SelectEnterEventArgs args)
    {
        isLeftHandleGrabbed = true;
        previousXRPosition = xrOrigin.position;
    }

    private void OnLeftHandleReleased(SelectExitEventArgs args)
    {
        isLeftHandleGrabbed = false;
    }

    private void OnRightHandleGrabbed(SelectEnterEventArgs args)
    {
        isRightHandleGrabbed = true;
        previousXRPosition = xrOrigin.position;
    }

    private void OnRightHandleReleased(SelectExitEventArgs args)
    {
        isRightHandleGrabbed = false;
    }
}