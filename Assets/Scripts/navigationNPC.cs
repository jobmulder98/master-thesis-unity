using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navigationNPC : MonoBehaviour
{
    // Array of checkpoints for the NPC to navigate through
    public Transform[] checkpoints;

    // Reference to the NavMeshAgent component
    private NavMeshAgent theAgent;

    // Index of the current checkpoint
    private int currentCheckpointIndex = 0;

    // Flag to indicate if the NPC is currently animating movement
    private bool isAnimating = false;

    void Start()
    {
        // Get the NavMeshAgent component attached to the GameObject
        theAgent = GetComponent<NavMeshAgent>();

        // If no checkpoints are provided, set the destination to the first checkpoint
        if (checkpoints.Length == 0)
        {
            theAgent.SetDestination(checkpoints[0].position);
        }
    }

    void Update()
    {
        // If the NPC is currently animating movement, do nothing
        if (isAnimating)
            return;

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
