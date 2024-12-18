using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public GameObject descriptionText;
   
    public void LoadThisScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ShowDescription()
    {
        descriptionText.SetActive(true);
    }
}
