using UnityEngine;
using UnityEngine.AI;

public class DeliveryAgent : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform holdPoint;
    [SerializeField] private Transform dropPoint; // Reference to where item should be dropped
    [SerializeField] private GameObject objectToPickup;
    [SerializeField] private float pickupRange = 1.5f;
    [SerializeField] private float dropRange = 1.5f;

    private bool isHoldingItem;
    private bool isDeliveringItem;
    private Vector3 joystickInput;

    private void Start()
    {
        if (agent == null) agent = GetComponent<NavMeshAgent>();
    }

    public void SetJoystickInput(Vector2 input)
    {
        joystickInput = new Vector3(input.x, 0, input.y);
    }

    private void Update()
    {
        if (isDeliveringItem)
        {
            // Move to drop point
            agent.SetDestination(dropPoint.position);
            
            // Check if close enough to drop
            if (Vector3.Distance(transform.position, dropPoint.position) < dropRange)
            {
                DropItem();
            }
        }
        else
        {
            // Normal joystick movement
            if (joystickInput.magnitude > 0.1f)
            {
                Vector3 moveDirection = joystickInput.normalized;
                Vector3 targetPosition = transform.position + moveDirection;
                agent.SetDestination(targetPosition);
            }

            // Check for pickup
            if (!isHoldingItem && objectToPickup != null)
            {
                float distanceToObject = Vector3.Distance(transform.position, objectToPickup.transform.position);
                if (distanceToObject < pickupRange)
                {
                    PickupItem();
                }
            }
        }
    }

    private void PickupItem()
    {
        objectToPickup.transform.SetParent(holdPoint);
        objectToPickup.transform.localPosition = Vector3.zero;
        objectToPickup.transform.localRotation = Quaternion.identity;

        isHoldingItem = true;
        isDeliveringItem = true;
    }

    private void DropItem()
    {
        // Position the item at the drop point
        objectToPickup.transform.SetParent(null);
        objectToPickup.transform.position = dropPoint.position;
        objectToPickup.transform.rotation = dropPoint.rotation;

        isHoldingItem = false;
        isDeliveringItem = false;
        Debug.Log("Item delivered to drop point!");
    }
}