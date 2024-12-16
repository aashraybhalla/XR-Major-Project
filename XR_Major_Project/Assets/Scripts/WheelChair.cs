using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WheelChair : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform wheelchairBody;
    [SerializeField] private XRGrabInteractable leftHandle;
    [SerializeField] private XRGrabInteractable rightHandle;
    
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float turnSpeed = 100f;
    [SerializeField] private float smoothing = 5f;
    
    private bool isLeftHandleGrabbed;
    private bool isRightHandleGrabbed;
    private Vector3 leftHandlePrevPos;
    private Vector3 rightHandlePrevPos;
    private float currentSpeed;
    private float targetSpeed;

    private void Start()
    {
        // Set up grab events for both handles
        leftHandle.selectEntered.AddListener(OnLeftHandleGrabbed);
        leftHandle.selectExited.AddListener(OnLeftHandleReleased);
        rightHandle.selectEntered.AddListener(OnRightHandleGrabbed);
        rightHandle.selectExited.AddListener(OnRightHandleReleased);
    }

    private void Update()
    {
        if (!isLeftHandleGrabbed && !isRightHandleGrabbed)
        {
            // Gradually slow down when not being pushed
            targetSpeed = 0f;
            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * smoothing);
            wheelchairBody.Translate(Vector3.forward * currentSpeed * Time.deltaTime, Space.Self);
            return;
        }

        // Calculate movement based on handle positions
        Vector3 leftHandleDelta = Vector3.zero;
        Vector3 rightHandleDelta = Vector3.zero;

        if (isLeftHandleGrabbed)
        {
            leftHandleDelta = leftHandle.transform.position - leftHandlePrevPos;
            leftHandlePrevPos = leftHandle.transform.position;
        }

        if (isRightHandleGrabbed)
        {
            rightHandleDelta = rightHandle.transform.position - rightHandlePrevPos;
            rightHandlePrevPos = rightHandle.transform.position;
        }

        // Calculate forward movement
        float forwardMovement = 0f;
        if (isLeftHandleGrabbed && isRightHandleGrabbed)
        {
            // Average movement when both handles are grabbed
            forwardMovement = (leftHandleDelta.z + rightHandleDelta.z) / 2f;
        }
        else
        {
            // Single handle movement
            forwardMovement = isLeftHandleGrabbed ? leftHandleDelta.z : rightHandleDelta.z;
        }

        // Calculate turning
        float turnAmount = 0f;
        if (isLeftHandleGrabbed && isRightHandleGrabbed)
        {
            // Turn based on handle difference
            turnAmount = (rightHandleDelta.z - leftHandleDelta.z) * turnSpeed;
        }

        // Apply movement
        targetSpeed = forwardMovement * moveSpeed;
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * smoothing);
        
        wheelchairBody.Translate(Vector3.forward * currentSpeed * Time.deltaTime, Space.Self);
        wheelchairBody.Rotate(Vector3.up * turnAmount * Time.deltaTime);
    }

    private void OnLeftHandleGrabbed(SelectEnterEventArgs args)
    {
        isLeftHandleGrabbed = true;
        leftHandlePrevPos = leftHandle.transform.position;
    }

    private void OnLeftHandleReleased(SelectExitEventArgs args)
    {
        isLeftHandleGrabbed = false;
    }

    private void OnRightHandleGrabbed(SelectEnterEventArgs args)
    {
        isRightHandleGrabbed = true;
        rightHandlePrevPos = rightHandle.transform.position;
    }

    private void OnRightHandleReleased(SelectExitEventArgs args)
    {
        isRightHandleGrabbed = false;
    }
}