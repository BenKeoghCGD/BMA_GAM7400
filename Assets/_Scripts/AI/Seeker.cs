using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Seeker : MonoBehaviour
{
    private NavMeshAgent _agent;

    public bool HasPath => _agent.hasPath;
    public float DistanceRemaining => _agent.remainingDistance;

    public void Init()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void SetPath(Vector3 destination)
    {
        _agent.destination = destination;
    }

    public void SetRandomPath(int searchRadius)
    {
        if(searchRadius <= 0)
        {
            Debug.LogError(gameObject.name + " has invalid path search radius");
            return;
        }

        Vector3 direction = Random.onUnitSphere * searchRadius;

        direction += transform.position;

        NavMeshHit hit;
        NavMesh.SamplePosition(direction, out hit, searchRadius, 1);

        _agent.destination = hit.position;
    }
}
