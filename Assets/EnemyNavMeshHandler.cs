using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyNavMeshHandler : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Transform Target;
    void Awake()
    {
        TryGetComponent<NavMeshAgent>(out agent);
    }
    void Update()
    {
        
        
        agent.destination = Target.position;
    }
}
