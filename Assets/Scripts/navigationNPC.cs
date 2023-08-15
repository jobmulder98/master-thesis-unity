using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navigationNPC : MonoBehaviour
{
    public Transform[] checkpoints;
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
                theAgent.SetDestination(checkpoints[currentCheckpointIndex].position);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
