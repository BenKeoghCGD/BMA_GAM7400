using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Waypoint : MonoBehaviour
{
    protected PathPlan _ownerPlan;

    protected bool isSwitch = false;
    public bool IsSwitch => isSwitch;

    protected bool isFinalWaypoint = false;
    public bool IsFinalWaypoint => isFinalWaypoint;

    public void SetOwnerPlan(PathPlan plan)
    {
        _ownerPlan = plan;
    }

    public void SetIsFinalWaypoint(bool val)
    {
        isFinalWaypoint = val;
    }
}
