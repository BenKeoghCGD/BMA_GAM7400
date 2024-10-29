using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent_Base : MonoBehaviour
{
    [SerializeField]
    protected int pathSearchRadius;
    [SerializeField]
    public LayerMask layerMask;

    protected LitterDropper litterDropper;
    protected Seeker seeker;

    protected AI_SpawnPoint spawnPoint;

    protected void Start()
    {
        if (gameObject.GetComponent<LitterDropper>() != null)
        {
            litterDropper = gameObject.GetComponent<LitterDropper>();
        }
        else
        {
            litterDropper = gameObject.AddComponent<LitterDropper>();
        }

        if (gameObject.GetComponent<Seeker>() != null)
        {
            seeker = gameObject.GetComponent<Seeker>();
        }
        else
        {
            seeker = gameObject.AddComponent<Seeker>();
        }

        seeker.Init();
    }

    protected void Destroy(SpawnPointType type)
    {
        GameManager.GetAISpawnManager().Decrement(type);
        Destroy(gameObject);

    }
    public void SetSpawnPoint(AI_SpawnPoint sP)
    {
        spawnPoint = sP;
    }
}
