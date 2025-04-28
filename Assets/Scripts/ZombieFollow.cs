using UnityEngine;
using UnityEngine.AI;

public class ZombieFollow : MonoBehaviour
{   
    Animator zombieAnim;
    public Transform player;  
    public float detectionRadius = 50f; // Detection range

    private NavMeshAgent agent;
    private float detectionRadiusSqr; // Store squared radius for better performance
    private void Awake(){
        zombieAnim = GetComponent<Animator>();
    }

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
            zombieAnim.SetBool("isFollowing",true);
            agent.SetDestination(player.position);
            
        }
        else{
            //zombieAnim.SetBool("isFollowing",false);
        }
        
    }
}
