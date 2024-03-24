using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCTrajectory : MonoBehaviour
{
    // Array of checkpoints defining the trajectory of the NPC
    public Transform[] checkpoints;

    // Reference to the Animator component
    private Animator animator;

    // Reference to the NavMeshAgent component
    private NavMeshAgent theAgent;

    // Index of the current checkpoint
    private int currentCheckpointIndex = 0;

    // Flag to indicate if the NPC is active and moving
    private bool isActive = false;

    void Start()
    {
        // Get the NavMeshAgent and Animator components attached to the GameObject
        theAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // If checkpoints are provided, set the destination to the first checkpoint
        if (checkpoints.Length > 0)
        {
            theAgent.SetDestination(checkpoints[currentCheckpointIndex].position);
        }
    }

    void Update()
    {
        // Check if the NPC has reached the current checkpoint
        if (theAgent.remainingDistance <= theAgent.stoppingDistance)
        {
            // If there are more checkpoints to visit, move to the next checkpoint
            if (currentCheckpointIndex < checkpoints.Length - 1)
            {
                // Increment the checkpoint index
                currentCheckpointIndex++;

                // Set the destination to the position of the next checkpoint
                theAgent.SetDestination(checkpoints[currentCheckpointIndex].position);
            }
            // If there are no more checkpoints, destroy the NPC GameObject
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
