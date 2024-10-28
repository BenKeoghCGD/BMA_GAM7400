using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Waypoint : MonoBehaviour
{
    //[SerializeField]
    //private AI_Waypoint _nextWaypoint;

    private bool _isFinalWaypoint = false;
    public bool IsFinalWaypoint => _isFinalWaypoint;
    // Start is called before the first frame update
    void Start()
    {
        //if(_nextWaypoint == null && _isFinalWaypoint == true)
        //{
        //    Debug.LogError("Waypoint '" + gameObject.name + "' is missing next waypoint");
        //    return;
        //}
    }

    //public AI_Waypoint GetNextWaypoint()
    //{
    //    if (_nextWaypoint == null && _isFinalWaypoint == true)
    //    {
    //        Debug.LogError("Waypoint '" + gameObject.name + "' is missing next waypoint");
    //        return null;
    //    }

    //    return _nextWaypoint;
    //}

    public void SetIsFinalWaypoint(bool val)
    {
        _isFinalWaypoint = val;
    }
}
