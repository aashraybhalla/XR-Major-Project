using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WheelChair : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform wheelchairBody;
    [SerializeField] private XRGrabInteractable leftHandle;
    [SerializeField] private XRGrabInteractable rightHandle;
    [SerializeField] private Transform xrOrigin; // Reference to XR Origin
    
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float smoothing = 5f;
    [SerializeField] private LayerMask groundLayer; // Layer mask for ground detection
    [SerializeField] private float groundCheckDistance = 0.1f; // Distance to check for ground
    
    private bool isLeftHandleGrabbed;
    private bool isRightHandleGrabbed;
    private Vector3 previousXRPosition;
    private float currentSpeed;
    private float targetSpeed;
    private Vector3 moveDirection;
    private Rigidbody wheelchairRigidbody;

    private void Start()
    {
        // Set up grab events for both handles
        leftHandle.selectEntered.AddListener(OnLeftHandleGrabbed);
        leftHandle.selectExited.AddListener(OnLeftHandleReleased);
        rightHandle.selectEntered.AddListener(OnRightHandleGrabbed);
        rightHandle.selectExited.AddListener(OnRightHandleReleased);

        // Get or add Rigidbody
        wheelchairRigidbody = GetComponent<Rigidbody>();
        if (wheelchairRigidbody == null)
        {
            wheelchairRigidbody = gameObject.AddComponent<Rigidbody>();
        }

        // Configure Rigidbody
        wheelchairRigidbody.useGravity = true;
        wheelchairRigidbody.constraints = RigidbodyConstraints.FreezeRotation; // Prevent tipping
        previousXRPosition = xrOrigin.position;
    }

    private void FixedUpdate()
    {
        if (!isLeftHandleGrabbed && !isRightHandleGrabbed)
        {
            // Gradually slow down when not being pushed
            targetSpeed = 0f;
            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.fixedDeltaTime * smoothing);
            return;
        }

        // Calculate XR Origin movement direction
        Vector3 xrMovement = xrOrigin.position - previousXRPosition;
        xrMovement.y = 0; // Remove vertical movement

        if (xrMovement.magnitude > 0.001f) // Check if there's significant movement
        {
            moveDirection = xrMovement.normalized;
        }

        // Calculate speed based on XR movement magnitude
        targetSpeed = xrMovement.magnitude * moveSpeed;
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.fixedDeltaTime * smoothing);

        // Apply movement using physics
        Vector3 targetVelocity = moveDirection * currentSpeed;
        targetVelocity.y = wheelchairRigidbody.velocity.y; // Preserve gravity
        wheelchairRigidbody.velocity = targetVelocity;

        // Ground check and snapping
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, groundCheckDistance + 1f, groundLayer))
        {
            Vector3 targetPosition = hit.point;
            targetPosition.y += groundCheckDistance; // Keep slight distance from ground
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.fixedDeltaTime * 10f);
        }

        // Update previous position
        previousXRPosition = xrOrigin.position;

        // Update wheelchair rotation to face movement direction
        if (targetVelocity.magnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * smoothing);
        }
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