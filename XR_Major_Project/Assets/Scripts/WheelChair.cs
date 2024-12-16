using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WheelchairController : MonoBehaviour
{
	[SerializeField] private XRGrabInteractable leftHandle;
	[SerializeField] private XRGrabInteractable rightHandle;
	[SerializeField] private Transform xrOrigin;
    
	private bool isGrabbed;
	private Vector3 originalPosition;
	private Quaternion originalRotation;

	private void Start()
	{
		leftHandle.selectEntered.AddListener(OnHandleGrabbed);
		leftHandle.selectExited.AddListener(OnHandleReleased);
		rightHandle.selectEntered.AddListener(OnHandleGrabbed);
		rightHandle.selectExited.AddListener(OnHandleReleased);
	}

	private void OnHandleGrabbed(SelectEnterEventArgs args)
	{
		if (!isGrabbed)
		{
			isGrabbed = true;
			// Store original position and rotation
			originalPosition = transform.position;
			originalRotation = transform.rotation;
            
			// Parent wheelchair to XR Origin
			transform.SetParent(xrOrigin);
		}
	}

	private void OnHandleReleased(SelectExitEventArgs args)
	{
		// Only unparent if both handles are released
		if (!leftHandle.isSelected && !rightHandle.isSelected)
		{
			isGrabbed = false;
			// Unparent from XR Origin
			transform.SetParent(null);
            
			// Maintain the world position/rotation it had while parented
			transform.position = transform.position;
			transform.rotation = transform.rotation;
		}
	}
}