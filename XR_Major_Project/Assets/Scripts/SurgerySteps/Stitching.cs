using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Stitching : MonoBehaviour
{
    public GameObject parentObject;
    private List<GameObject> outlines = new List<GameObject>();

    private int currentCount;
    public Material newMaterial;

    [Header("Audio")]
    public AudioSource audioSource;

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
                audioSource.Play();
                this.enabled = false;
            }
        }
    }
}
