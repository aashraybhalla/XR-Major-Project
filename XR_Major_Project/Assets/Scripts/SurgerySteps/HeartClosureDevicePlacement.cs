using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HeartClosureDevicePlacement : MonoBehaviour
{
    public GameObject heart;
    public GameObject heartPos;
    private Transform self;
    private XRGrabInteractable interactable;
    public GameObject skinBody;
    // Start is called before the first frame update
    void Start()
    {
        self = GetComponent<Transform>();
        interactable = GetComponent<XRGrabInteractable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HeartPosition"))
        {
            interactable.enabled = false;
            self.transform.position = other.transform.position;
            Invoke("DelayActions", 3f);
        }
    }

    private void DelayActions()
    {
        heartPos.SetActive(false);
        heart.SetActive(false);
        skinBody.SetActive(true);
        //enable next script
    }
}
