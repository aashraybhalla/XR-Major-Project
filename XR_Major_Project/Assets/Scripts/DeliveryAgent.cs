using UnityEngine;
using UnityEngine.AI;

public class DeliveryAgent : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform holdPoint;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject objectToPickup; // Reference to the specific object to pick up
    [SerializeField] private float pickupRange = 1.5f;
    [SerializeField] private float deliveryRange = 2f;

    private bool isHoldingItem;
    private bool isReturningToPlayer;
    private Vector3 joystickInput;

    private void Start()
    {
        if (agent == null) agent = GetComponent<NavMeshAgent>();
    }

    // Call this from your joystick script
    public void SetJoystickInput(Vector2 input)
    {
        joystickInput = new Vector3(input.x, 0, input.y);
    }

    private void Update()
    {
        if (isReturningToPlayer)
        {
            agent.SetDestination(playerTransform.position);
            
            if (Vector3.Distance(transform.position, playerTransform.position) < deliveryRange)
            {
                DeliverItem();
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
        isReturningToPlayer = true;
    }

    private void DeliverItem()
    {
        objectToPickup.transform.SetParent(null);
        isHoldingItem = false;
        isReturningToPlayer = false;
        Debug.Log("Item delivered to player!");
    }
}