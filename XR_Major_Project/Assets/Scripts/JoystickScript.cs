using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
public class JoystickScript : MonoBehaviour
{
    public XRJoystick joystick;
    public float moveSpeed = 5f;
    
    public DeliveryAgent deliveryAgent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(joystick.value.x, 0f, joystick.value.y)* moveSpeed * Time.deltaTime;

        transform.Translate(movement);
        UpdateJoystick(joystick.value);
    }
    
    void UpdateJoystick(Vector2 input)
    {
        deliveryAgent.SetJoystickInput(input);
    }
}
