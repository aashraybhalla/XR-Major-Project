using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WheelChair : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable leftHandle;
    [SerializeField] private XRGrabInteractable rightHandle;
    [SerializeField] private Transform xrOrigin;
    
    [Header("Handle References")]
    [SerializeField] private Transform leftHandleTransform;  // Parent transform of left handle
    [SerializeField] private Transform rightHandleTransform; // Parent transform of right handle
    
    private bool isLeftHandleGrabbed;
    private bool isRightHandleGrabbed;
    private Vector3 leftHandleLocalPos;
    private Vector3 rightHandleLocalPos;
    private Quaternion leftHandleLocalRot;
    private Quaternion rightHandleLocalRot;

    private void Start()
    {
        // Store initial local positions and rotations of handles
        leftHandleLocalPos = leftHandleTransform.localPosition;
        rightHandleLocalPos = rightHandleTransform.localPosition;
        leftHandleLocalRot = leftHandleTransform.localRotation;
        rightHandleLocalRot = rightHandleTransform.localRotation;

        leftHandle.selectEntered.AddListener(OnLeftHandleGrabbed);
        leftHandle.selectExited.AddListener(OnLeftHandleReleased);
        rightHandle.selectEntered.AddListener(OnRightHandleGrabbed);
        rightHandle.selectExited.AddListener(OnRightHandleReleased);
    }

    private void OnLeftHandleGrabbed(SelectEnterEventArgs args)
    {
        isLeftHandleGrabbed = true;
        CheckBothHandlesGrabbed();
    }

    private void OnLeftHandleReleased(SelectExitEventArgs args)
    {
        isLeftHandleGrabbed = false;
        DetachFromXROrigin();
        // Reset handle position
        ResetHandlePositions();
    }

    private void OnRightHandleGrabbed(SelectEnterEventArgs args)
    {
        isRightHandleGrabbed = true;
        CheckBothHandlesGrabbed();
    }

    private void OnRightHandleReleased(SelectExitEventArgs args)
    {
        isRightHandleGrabbed = false;
        DetachFromXROrigin();
        // Reset handle position
        ResetHandlePositions();
    }

    private void CheckBothHandlesGrabbed()
    {
        if (isLeftHandleGrabbed && isRightHandleGrabbed)
        {
            // Parent wheelchair to XR Origin, but keep handles as children of wheelchair
            transform.SetParent(xrOrigin);
        }
    }

    private void DetachFromXROrigin()
    {
        if (!isLeftHandleGrabbed || !isRightHandleGrabbed)
        {
            transform.SetParent(null);
            // Maintain world position and rotation
            transform.position = transform.position;
            transform.rotation = transform.rotation;
        }
    }

    private void ResetHandlePositions()
    {
        // Ensure handles maintain their positions relative to wheelchair
        leftHandleTransform.localPosition = leftHandleLocalPos;
        rightHandleTransform.localPosition = rightHandleLocalPos;
        leftHandleTransform.localRotation = leftHandleLocalRot;
        rightHandleTransform.localRotation = rightHandleLocalRot;
    }
}