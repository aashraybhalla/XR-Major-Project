using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.XR.Interaction.Toolkit;

public class HeartClosureDevicePlacement : MonoBehaviour
{
    public GameObject heart;
    public GameObject heartPos;
    private Transform self;
    private XRGrabInteractable interactable;
    public GameObject skinBody;
    public Stitching stitching;

    [Header("Video")]
    public VideoPlayer videoPlayer;
    public VideoClip clip;
    private bool hasVideoPlayed = false;


    // Start is called before the first frame update
    void Start()
    {
        self = GetComponent<Transform>();
        interactable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        if (!hasVideoPlayed)
        {
            videoPlayer.clip = clip;
            videoPlayer.Play();
            hasVideoPlayed = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("HeartPosition"))
        {
            interactable.enabled = false;
            self.transform.position = other.transform.position;
            self.transform.rotation = other.transform.rotation;
            Invoke("DelayActions", 3f);
            this.enabled = false;
        }
    }

    private void DelayActions()
    {
        heartPos.SetActive(false);
        heart.SetActive(false);
        skinBody.SetActive(true);
        //enable next script
        stitching.enabled = true;
    }
}
