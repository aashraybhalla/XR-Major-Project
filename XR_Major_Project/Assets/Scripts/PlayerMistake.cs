using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PublicMistake : MonoBehaviour
{
    // References to the components to check
    public Scissors scissors;
    public Drill drill;
    public HeartClosureDevicePlacement devicePlacement;
    public Scalpel scalpel;
    public Anaesthesia anaesthesia;
    public Stitching stitching;

    // Audio source for playing the sound effect
    public AudioSource audioSource;

    // Reference to XR Controller for haptic feedback
    public XRBaseController rightController; // Assign in Inspector
    public XRBaseController leftController; // Assign in Inspector

    // Function to check the component's state
    public void CheckComponentState()
    {
        Behaviour targetComponent = null;

        // Check each component in priority order and assign the first non-null one
        if (scissors != null)
        {
            targetComponent = scissors;
        }
        else if (drill != null)
        {
            targetComponent = drill;
        }
        else if (devicePlacement != null)
        {
            targetComponent = devicePlacement;
        }
        else if (scalpel != null)
        {
            targetComponent = scalpel;
        }
        else if (anaesthesia != null)
        {
            targetComponent = anaesthesia;
        }
        else if(stitching != null)
        {
            targetComponent = stitching;
        }

        // If a valid component is found and it's disabled
        if (!targetComponent.enabled || targetComponent == null)
        {
            audioSource.Play();

            // Trigger haptic feedback
            TriggerHaptic();
        }
    }

    // Function to send haptic feedback to the controller
    private void TriggerHaptic()
    {
        if (rightController != null || leftController != null)
        {
            // Haptic feedback parameters
            float amplitude = 0.5f; // Strength (0 to 1)
            float duration = 0.2f; // Time in seconds

            // Send haptic impulse to the controller
            rightController.SendHapticImpulse(amplitude, duration);
            leftController.SendHapticImpulse(amplitude, duration);
        }
    }
}

