using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour
{
    private List<GameObject> outlines = new List<GameObject>();

    private int currentCount;
    public Material newMaterial;
    public GameObject parentObject;

    public GameObject heart;
    public GameObject devicePos;
    public HeartClosureDevicePlacement devicePlacement;
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
    private void OnEnable()
    {
        //currentCount = 0;
        //foreach (Transform child in parentObject.transform)
        //{
        //    outlines.Add(child.gameObject);
        //}
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

                heart.SetActive(true);
                devicePos.SetActive(true);
                devicePlacement.enabled = true;
                this.enabled = false;
            }
        }

    }
}
