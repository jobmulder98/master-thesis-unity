using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCTrajectory : MonoBehaviour
{
    public Transform[] checkpoints;
    private Animator animator;
    private NavMeshAgent theAgent;
    private int currentCheckpointIndex = 0;
    private bool isActive = false;

    void Start()
    {
        theAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (checkpoints.Length > 0)
        {
            theAgent.SetDestination(checkpoints[currentCheckpointIndex].position);
        }
    }

    void Update()
    {
        if (theAgent.remainingDistance <= theAgent.stoppingDistance)
        {
            if (currentCheckpointIndex < checkpoints.Length - 1)
            {
                currentCheckpointIndex++;
                theAgent.SetDestination(checkpoints[currentCheckpointIndex].position);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
