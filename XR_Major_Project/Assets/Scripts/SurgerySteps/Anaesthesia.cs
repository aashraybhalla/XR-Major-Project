using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anaesthesia : MonoBehaviour
{
    private List<GameObject> outlines = new List<GameObject>();

    private int currentCount;
    public GameObject parentObject;
    public Scissors scissors;
    // Start is called before the first frame update
    void Start()
    {
        currentCount = 0;
        foreach (Transform child in parentObject.transform)
        {
            outlines.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == outlines[currentCount])
        {
            currentCount++;

            if (currentCount >= outlines.Count)
            {
                foreach (GameObject go in outlines)
                {
                    Destroy(go);
                }
                scissors.enabled = true;
            }
        }

    }
}
