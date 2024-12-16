using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scalpel : MonoBehaviour
{
    private List<GameObject> outlines = new List<GameObject>();

    private int currentCount;
    public Material newMaterial;
    public GameObject parentObject;
    public Drill drill;
    public GameObject patientSkinBody;

    // Start is called before the first frame update
    void Start()
    {
        parentObject.SetActive(true);
        currentCount = 0;
        foreach (Transform child in parentObject.transform)
        {
            outlines.Add(child.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == outlines[currentCount])
        {
            Renderer objRender = other.GetComponent<Renderer>();
            objRender.material = newMaterial;
            currentCount++;

            if (currentCount >= outlines.Count)
            {
                foreach (GameObject go in outlines)
                {
                    Destroy(go);
                }

                patientSkinBody.SetActive(false);
                drill.enabled = true;
                this.enabled = false;
            }
        }

    }
}
