using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyNavMeshHandler : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform Target;

    void Update()
    {
        
        agent.destination = Target.position;

    }

 void OnDrawGizmos()
{
    foreach (var corner in agent.path.corners)
    {
        Gizmos.DrawSphere(corner,.1f);
    }
}

}
