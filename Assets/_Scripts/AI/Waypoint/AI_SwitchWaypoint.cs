using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_SwitchWaypoint : AI_Waypoint
{
    [SerializeField]
    private PathPlan _potentialPlan;
    [SerializeField]
    private float _chanceToSwitch;

    [SerializeField]
    private int _listBuffer;

    private void Start()
    {
        isSwitch = true;
    }
    public List<AI_Waypoint> GetNextPlan()
    {
        float random = Random.Range(0, 100);

        if(random <= _chanceToSwitch)
        {
            List<AI_Waypoint> newPlan = _potentialPlan.GetPath();

            if(_listBuffer > 0)
            {
                for(int i = 0; i <  _listBuffer; i++)
                {
                    newPlan.RemoveAt(0);
                }
            }

            return newPlan;
        }

        return null;
    }
}
