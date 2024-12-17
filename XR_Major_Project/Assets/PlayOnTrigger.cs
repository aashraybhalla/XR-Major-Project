using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnTrigger : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string triggerName;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetTrigger(triggerName);
        }
    }
}
