using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class CombinationLock : MonoBehaviour
{
    [Header("Lock Settings")]
    [SerializeField] private string correctCombination = "1234"; // Set your combination in inspector
    [SerializeField] private float resetDelay = 2f; // Time before combination resets if incorrect
    [SerializeField] private int maxAttempts = 3; // Maximum number of attempts before lockout
    
    [Header("Events")]
    [SerializeField] private UnityEvent onCorrectCombination;
    [SerializeField] private UnityEvent onIncorrectCombination;
    [SerializeField] private UnityEvent onMaxAttemptsReached;
    
    [Header("References")]
    [SerializeField] private Animator lockAnimator;
    
    
    private List<string> currentInput = new List<string>();
    private int attemptCount = 0;
    private bool isLocked = false;

    // Call this method from your button click events
    public void OnButtonPressed(string number)
    {
        if (isLocked) return;

        // Add number to current input
        currentInput.Add(number);
        Debug.Log($"Button pressed: {number}. Current input: {string.Join("", currentInput)}");

        // Check if we've reached the correct length
        if (currentInput.Count == correctCombination.Length)
        {
            CheckCombination();
        }
    }

    private void CheckCombination()
    {
        string attempt = string.Join("", currentInput);

        if (attempt == correctCombination)
        {
            Debug.Log("Correct combination!");
            onCorrectCombination?.Invoke();
            ResetLock();
            attemptCount = 0; // Reset attempt count on success
        }
        else
        {
            Debug.Log("Incorrect combination!");
            attemptCount++;
            onIncorrectCombination?.Invoke();

            if (attemptCount >= maxAttempts)
            {
                Debug.Log("Maximum attempts reached!");
                isLocked = true;
                onMaxAttemptsReached?.Invoke();
            }
            
            // Reset after delay
            Invoke(nameof(ResetLock), resetDelay);
        }
    }

    public void ResetLock()
    {
        currentInput.Clear();
        Debug.Log("Lock reset");
    }

    // Call this to reset the lockout state
    public void ResetLockout()
    {
        isLocked = false;
        attemptCount = 0;
        ResetLock();
    }

    // Optional: Method to change combination at runtime
    public void SetCombination(string newCombination)
    {
        correctCombination = newCombination;
        ResetLock();
    }
    
    public void OnCorrectCombination()
    {
        Debug.Log("Correct combination entered!");
        lockAnimator.SetTrigger("Unlock");
    }
    public void OnIncorrectCombination()
    {
        Debug.Log("Incorrect combination entered!");
        //lockAnimator.SetTrigger("Shake");
    }
    
    public void OnMaxAttemptsReached()
    {
        Debug.Log("Max attempts reached!");
        //lockAnimator.SetTrigger("Lockout");
    }
}