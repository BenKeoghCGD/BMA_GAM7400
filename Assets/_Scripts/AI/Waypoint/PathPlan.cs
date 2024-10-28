using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPlan : MonoBehaviour
{
    [SerializeField]
    private List<AI_Waypoint> _path;

    private void Start()
    {
        foreach(AI_Waypoint waypoint in _path)
        {
            waypoint.SetOwnerPlan(this);
        }
    }

    public List<AI_Waypoint> GetPath()
    {
        return _path;
    }
}
