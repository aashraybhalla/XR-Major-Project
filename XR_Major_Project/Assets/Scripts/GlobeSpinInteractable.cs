using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GlobeSpinInteractable : XRBaseInteractable
{
    private Transform interactorTransform; 
    private Vector3 lastInteractorPosition; 

    [Tooltip("Enable free rotation around X and Y axes (disable for Y-axis only).")]
    public bool freeSpin = false; 

    private Rigidbody rb; 
    private float angularDrag = 3f; 
    private float rotationSpeed = 300f; 
    private bool isInteracting = false; 

    private Vector3 lastAngularVelocity; 

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);

        interactorTransform = args.interactorObject.transform;
        lastInteractorPosition = interactorTransform.position;

        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.angularVelocity = Vector3.zero; 
            rb.angularDrag = 0f; 
        }

        isInteracting = true;
    }

    private void Update()
    {
        if (interactorTransform != null && isInteracting)
        {
            Vector3 currentInteractorPosition = interactorTransform.position;
            Vector3 movementDelta = currentInteractorPosition - lastInteractorPosition;

            if (freeSpin)
            {
                Vector3 effectiveRotationAxis = Vector3.Cross(transform.position - interactorTransform.position, movementDelta);
                transform.Rotate(effectiveRotationAxis.normalized, movementDelta.magnitude * rotationSpeed, Space.World);
            }
            else
            {
                float yDelta = movementDelta.x; 
                transform.Rotate(Vector3.up, yDelta * rotationSpeed, Space.World);
            }

            lastInteractorPosition = currentInteractorPosition;
        }
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        base.OnSelectExiting(args);

        interactorTransform = null;

        if (rb != null)
        {
            lastAngularVelocity = rb.angularVelocity; 
            rb.angularDrag = angularDrag; 
            isInteracting = false;
        }
    }

    private void FixedUpdate()
    {
        if (rb != null && !isInteracting)
        {
            if (lastAngularVelocity != Vector3.zero)
            {
                rb.angularVelocity = lastAngularVelocity; 

                if (rb.angularVelocity.magnitude < 0.1f)
                {
                    rb.angularVelocity = Vector3.zero; 
                }
            }
        }
    }
}
