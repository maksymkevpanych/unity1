using UnityEngine;
using UnityEngine.AI;

public class ZombieFollow : MonoBehaviour
{
    public Transform player;  
    public float detectionRadius = 50f; // Detection range

    private NavMeshAgent agent;
    private float detectionRadiusSqr; // Store squared radius for better performance
    

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Precompute squared values for optimization
        detectionRadiusSqr = detectionRadius * detectionRadius;
        
    }

    void Update()
    {
        float distanceSqr = (player.position - transform.position).sqrMagnitude; // Avoids costly sqrt()

        if (distanceSqr <= detectionRadiusSqr)
        {
            agent.SetDestination(player.position);
            
        }
        
    }
}
