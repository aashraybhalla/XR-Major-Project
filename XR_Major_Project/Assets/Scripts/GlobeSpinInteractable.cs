using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GlobeSpinInteractable : XRBaseInteractable
{
    private Transform interactorTransform; // The interactor interacting with the globe
    private Vector3 lastInteractorPosition; // The last known position of the interactor

    [Tooltip("Enable free rotation around X and Y axes (disable for Y-axis only).")]
    public bool freeSpin = false; // Default to Y-axis only

    private Rigidbody rb; // The Rigidbody attached to the globe for physics-based movement
    private float angularDrag = 3f; // The amount of drag that gradually slows the object
    private float rotationSpeed = 300f; // Adjust for sensitivity
    private bool isInteracting = false; // Whether the player is currently interacting with the object

    private Vector3 lastAngularVelocity; // To store the angular velocity at the moment of release

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);

        // Store the interactor's transform and position
        interactorTransform = args.interactorObject.transform;
        lastInteractorPosition = interactorTransform.position;

        // Ensure Rigidbody is available
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.angularVelocity = Vector3.zero; // Stop any spinning before interaction
            rb.angularDrag = 0f; // Disable angular drag while interacting
        }

        isInteracting = true;
    }

    private void Update()
    {
        if (interactorTransform != null && isInteracting)
        {
            // Calculate movement of the interactor
            Vector3 currentInteractorPosition = interactorTransform.position;
            Vector3 movementDelta = currentInteractorPosition - lastInteractorPosition;

            // Constrain rotation to the Y-axis
            if (freeSpin)
            {
                // Free rotation based on interactor movement
                Vector3 effectiveRotationAxis = Vector3.Cross(transform.position - interactorTransform.position, movementDelta);
                transform.Rotate(effectiveRotationAxis.normalized, movementDelta.magnitude * rotationSpeed, Space.World);
            }
            else
            {
                // Constrain rotation to the Y-axis
                float yDelta = movementDelta.x; // Horizontal movement affects Y-axis rotation
                transform.Rotate(Vector3.up, yDelta * rotationSpeed, Space.World);
            }

            // Update the last interactor position
            lastInteractorPosition = currentInteractorPosition;
        }
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        base.OnSelectExiting(args);

        // Clear the interactor reference
        interactorTransform = null;

        // Store the current angular velocity when the player releases the object
        if (rb != null)
        {
            lastAngularVelocity = rb.angularVelocity; // Capture the angular velocity
            rb.angularDrag = angularDrag; // Apply angular drag to slow down
            isInteracting = false;
        }
    }

    private void FixedUpdate()
    {
        // If the object is no longer interacting, apply the captured angular velocity and apply drag
        if (rb != null && !isInteracting)
        {
            if (lastAngularVelocity != Vector3.zero)
            {
                rb.angularVelocity = lastAngularVelocity; // Continue spinning with the momentum

                // Gradually slow down the rotation (apply drag)
                if (rb.angularVelocity.magnitude < 0.1f)
                {
                    rb.angularVelocity = Vector3.zero; // Stop if the velocity is very low
                }
            }
        }
    }
}
