using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossingPoint : MonoBehaviour
{
    protected Crossing _crossing;
    public bool CanCross => _crossing.canCross;

    public void SetCrossing(Crossing crossing)
    {
        _crossing = crossing;
    }
    public Vector3 GetCrossingPosition()
    {
        return new Vector3(_crossing.transform.position.x, 1, _crossing.transform.position.y);
    }
}
