using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class Scissors : MonoBehaviour
{
    private List<GameObject> outlines = new List<GameObject>();

    private int currentCount;
    public Material newMaterial;
    public GameObject parentObject;
    public Scalpel scalpel;
    public GameObject patientBody;
    // Start is called before the first frame update
    void Start()
    {
        currentCount = 0;
        parentObject.SetActive(true);
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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == outlines[currentCount])
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

                patientBody.SetActive(false);
                scalpel.enabled = true;
            }
        }
        
    }
}