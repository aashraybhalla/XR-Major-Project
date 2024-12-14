using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Stitching : MonoBehaviour
{
    [Header("Stitching Points")]
    public List<Collider> stitchingPoints; // List of stitching point colliders in order

    [Header("Needle Reference")]
    public XRGrabInteractable needle; // Reference to the needle with XR Grab Interactable

    private int currentPointIndex = 0; // Tracks the current stitching point

    private void OnEnable()
    {
        if (needle != null)
        {
            needle.selectEntered.AddListener(OnNeedleGrabbed);
        }
    }

    private void OnDisable()
    {
        if (needle != null)
        {
            needle.selectEntered.RemoveListener(OnNeedleGrabbed);
        }
    }

    private void OnNeedleGrabbed(SelectEnterEventArgs args)
    {
        Debug.Log("Needle grabbed. Ready for stitching.");
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the current stitching point
        if (currentPointIndex < stitchingPoints.Count && other == stitchingPoints[currentPointIndex])
        {
            Debug.Log($"Stitching point {currentPointIndex + 1} completed.");

            // Advance to the next stitching point
            currentPointIndex++;

            // Check if all stitching points are completed
            if (currentPointIndex >= stitchingPoints.Count)
            {
                Debug.Log("Stitching completed successfully!");
                OnStitchingCompleted();
            }
        }
        else
        {
            Debug.Log("Wrong stitching point or out of order.");
        }
    }

    private void OnStitchingCompleted()
    {
        // Add any logic for when stitching is fully completed
        Debug.Log("You have successfully sutured the wound!");
    }
}
