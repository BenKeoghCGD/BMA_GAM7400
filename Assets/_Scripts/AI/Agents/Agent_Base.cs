using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent_Base : MonoBehaviour
{
    [SerializeField]
    protected int pathSearchRadius;

    protected Seeker seeker;

    protected void Start()
    {
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
}
