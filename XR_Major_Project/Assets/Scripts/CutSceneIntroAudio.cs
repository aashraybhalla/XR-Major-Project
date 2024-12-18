using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneIntroAudio : MonoBehaviour
{
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip BGMusic;

    // Start is called before the first frame update
    void Start()
    {
        audioSource2.clip = BGMusic;
        audioSource2.Play();
        Invoke("PlayClip1", 2f);
    }

    void PlayClip1()
    {
        audioSource1.clip = clip1;
        audioSource1.Play();
        Invoke("PlayClip2", 22f);
    }

    void PlayClip2()
    {
        audioSource1.clip = clip2;
        audioSource1.Play();
        Invoke("PlayClip3", 23f);
    }

    void PlayClip3()
    {
        audioSource1.clip = clip3;
        audioSource1.Play();
        Invoke("LoadScene", 25f);
    }

    void LoadScene()
    {
        SceneManager.LoadScene("Scene_Main");
    }
}
