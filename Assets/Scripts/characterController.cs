using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class characterController : MonoBehaviour
{
    public Transform[] checkpoints; // Array of checkpoints the character will traverse
    public int actionAtCheckpoint = 1; // Index of checkpoint where character performs an action
    public string action; // Name of the action trigger parameter in the animator
    public float stopDuration = 3.6f; // Duration to stop at the checkpoint

    private Animator animator; // Reference to the character's animator component
    private NavMeshAgent theAgent; // Reference to the NavMeshAgent component for navigation
    private int currentCheckpointIndex = 0; // Index of the current checkpoint
    private bool isAnimating = false; // Flag to track if character is currently animating

    void Start()
    {
        // Get references to components
        theAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Set initial destination if there are checkpoints
        if (checkpoints.Length > 0)
        {
            theAgent.SetDestination(checkpoints[0].position);
        }
    }

    void Update()
    {
        // If character is currently animating, do not proceed with navigation
        if (isAnimating)
            return;

        // Check if the character has reached the current checkpoint
        if (theAgent.remainingDistance <= theAgent.stoppingDistance)
        {
            // If there are more checkpoints to traverse
            if (currentCheckpointIndex < checkpoints.Length - 1)
            {
                currentCheckpointIndex++;

                // If the current checkpoint is the one where the character should perform an action
                if (currentCheckpointIndex == actionAtCheckpoint)
                {
                    // Start animation and wait at the checkpoint
                    StartCoroutine(AnimateAndWait());
                    return;
                }
                else
                {
                    // Move to the next checkpoint
                    theAgent.SetDestination(checkpoints[currentCheckpointIndex].position);
                }
            }
            else
            {
                // Destroy the character if all checkpoints are traversed
                Destroy(gameObject);
            }
        }
    }

    // Coroutine to animate the character and wait at the checkpoint
    IEnumerator AnimateAndWait()
    {
        theAgent.isStopped = true; // Stop navigation
        isAnimating = true; // Set animating flag

        // Trigger animation
        animator.SetBool(action, true);

        // Wait for the specified duration
        yield return new WaitForSeconds(stopDuration);

        // End animation and resume navigation
        animator.SetBool(action, false);
        theAgent.isStopped = false;
        isAnimating = false;

        // Proceed to the next checkpoint
        theAgent.SetDestination(checkpoints[currentCheckpointIndex].position);
    }

    // Placeholder function to prevent error in Unity editor for predefined animation events
    public void NewEvent()
    {
        return;
    }
}
