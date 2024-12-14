using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public enum HandType
{
    Left,
    Right
}

public class XRHandController : MonoBehaviour
{
    public HandType handType;

    private Animator animator;
    private InputDevice inputDevice;

    private float pinchValue;
    private float indexValue;

    void Start()
    {
        animator = GetComponent<Animator>();
        inputDevice = GetInputDevice();
    }

    // Update is called once per frame
    void Update()
    {
        AnimateHand();
    }

    InputDevice GetInputDevice()
    {
        InputDeviceCharacteristics controllerCharacteristic = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller;

        if (handType == HandType.Left)
        {
            controllerCharacteristic = controllerCharacteristic | InputDeviceCharacteristics.Left;
        }
        else
        {
            controllerCharacteristic = controllerCharacteristic | InputDeviceCharacteristics.Right;
        }

        List<InputDevice> inputDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristic, inputDevices);

        return inputDevices.Count > 0 ? inputDevices[0] : default;
    }

    void AnimateHand()
    {
        bool hasTriggerValue = inputDevice.TryGetFeatureValue(CommonUsages.trigger, out indexValue);
        bool hasGripValue = inputDevice.TryGetFeatureValue(CommonUsages.grip, out pinchValue);

        // Reset animation values
        animator.SetFloat("Index", 0);
        animator.SetFloat("Pinch", 0);

        // Prioritize trigger animation over grip
        if (hasTriggerValue && indexValue > 0)
        {
            animator.SetFloat("Index", indexValue);
        }
        else if (hasGripValue && pinchValue > 0)
        {
            animator.SetFloat("Pinch", pinchValue);
        }
    }
}
