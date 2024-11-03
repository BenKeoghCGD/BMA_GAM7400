using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mesh;

public class PlaceableLitter : Litter
{
    void Start()
    {
        GameManager.GetLitterManager().AddLitter(this);
        gameObject.transform.position = new Vector3(transform.position.x, 1, transform.position.z);

        Init(GameManager.GetLitterManager().GetRandomLitterData(), gameObject);
    }
}
