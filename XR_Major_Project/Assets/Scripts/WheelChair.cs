using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WheelchairController : MonoBehaviour
{
	[SerializeField] private XRGrabInteractable leftHandle;
	[SerializeField] private XRGrabInteractable rightHandle;
	[SerializeField] private Transform xrOrigin;
    
	private bool isLeftHandleGrabbed;
	private bool isRightHandleGrabbed;

	private void Start()
	{
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
	}

	private void CheckBothHandlesGrabbed()
	{
		if (isLeftHandleGrabbed && isRightHandleGrabbed)
		{
			transform.SetParent(xrOrigin);
		}
	}

	private void DetachFromXROrigin()
	{
		transform.SetParent(null);
		// Maintain the world position/rotation it had while parented
		transform.position = transform.position;
		transform.rotation = transform.rotation;
	}
}