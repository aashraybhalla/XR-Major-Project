using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Rotation speed for the sphere (adjustable in the Inspector)
    public Vector3 rotationSpeed = new Vector3(0f, 50f, 0f);

    void Update()
    {
        // Rotate the sphere over time
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
