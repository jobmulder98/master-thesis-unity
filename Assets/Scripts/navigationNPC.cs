using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navigationNPC : MonoBehaviour
{
    public Transform[] checkpoints;
    public Animator animator;
    public int actionAtCheckpoint;
    public string action;

    private NavMeshAgent theAgent;
    private int currentCheckpointIndex = 0;
    private bool isAnimating = false;

    void Start()
    {
        theAgent = GetComponent<NavMeshAgent>();
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
                    theAgent.isStopped = true;
                    isAnimating = true;
                    animator.SetTrigger(action);
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
}
