using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class characterController : MonoBehaviour
{
    public Transform[] checkpoints;
    public int actionAtCheckpoint = 1;
    public string action;
    public float stopDuration = 3.6f; // Duration to stop at the checkpoint

    private Animator animator;
    private NavMeshAgent theAgent;
    private int currentCheckpointIndex = 0;
    private bool isAnimating = false;

    void Start()
    {
        theAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        if (checkpoints.Length == 0)
        {
            theAgent.SetDestination(checkpoints[0].position);
        }
    }

    void Update()
    {
        if (isAnimating)
            return;

        if (theAgent.remainingDistance <= theAgent.stoppingDistance)
        {
            if (currentCheckpointIndex < checkpoints.Length - 1)
            {
                currentCheckpointIndex++;

                if (currentCheckpointIndex == actionAtCheckpoint)
                {
                    StartCoroutine(AnimateAndWait());
                    return;
                }
                else
                {
                    theAgent.SetDestination(checkpoints[currentCheckpointIndex].position);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator AnimateAndWait()
    {
        theAgent.isStopped = true;
        isAnimating = true;
        animator.SetBool(action, true);

        // Wait for the specified duration
        yield return new WaitForSeconds(stopDuration);

        // Resume movement
        animator.SetBool(action, false);
        theAgent.isStopped = false;
        isAnimating = false;

        // Proceed to the next checkpoint
        theAgent.SetDestination(checkpoints[currentCheckpointIndex].position);
    }

    public void NewEvent()
    {
        return; // Event is created for predefined animation. Bug does nothing but is annoying, this function removes the error in Unity.
    }
}
