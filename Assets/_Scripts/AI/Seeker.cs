using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Seeker : MonoBehaviour
{
    [SerializeField]
    public NavMeshAgent agent;

    [SerializeField]
    private int pathSearchRadius;

    bool test;
    // Start is called before the first frame update
    void Start()
    {
        test = false;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.hasPath != true)
        {
            agent.destination = GetRandomPoint();

            test = true;
        }
    }

    Vector3 GetRandomPoint()
    {
        Vector3 direction = Random.onUnitSphere * pathSearchRadius;

        direction += transform.position;

        NavMeshHit hit;
        NavMesh.SamplePosition(direction, out hit, pathSearchRadius, 1);

        return hit.position;
    }
}
