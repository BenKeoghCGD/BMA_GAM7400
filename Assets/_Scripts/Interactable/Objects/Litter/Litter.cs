﻿/*
 * Branch: Main (Keogh, Ben)
 * Commit: c5c64a33b28ef4617eae3f6b5dcc3374872a0938
 * 
 * Cleaned 9/10/24 (Keogh, Ben)
 * Branch: Main, Stable (Keogh, Ben)
 * Commit: c5c64a33b28ef4617eae3f6b5dcc3374872a0938
 */

// Updated 14/10/24 (Higham, Ben), changes commented

using UnityEngine;

public class Litter : MonoBehaviour, IInteractable
{
    [SerializeField] 
    public LitterType litterType;

    private Vector3 _initialSpawnpoint;

    protected GameObject litterObj;

    protected int litterSize;
    public int LitterSize => litterSize;

    private Animator pickUp;
    protected AudioClip pickUpSound;

    public void Init(LitterData data, GameObject obj)
    {
        if (data == null)
        {
            Debug.LogError("Tried to create litter with null data.");
            return;
        }

        if (obj == null)
        {
            Debug.LogError("Object not assigned to litter");
            return;
        }

        litterObj = obj;

        litterObj.name = data.litterName;

        litterObj.AddComponent<MeshFilter>().mesh = data.litterModel;
        litterObj.AddComponent<MeshRenderer>().material = data.litterMaterial;

        litterSize = data.litterSize;
        litterType = data.litterType;

        pickUpSound = data.litterSound;

        litterObj.transform.localScale = new Vector3(3, 3, 3);
        litterObj.AddComponent<BoxCollider>().isTrigger = true;

        _initialSpawnpoint = gameObject.transform.position;
    }

    public void SetLitterType(LitterType type)
    {
        litterType = type;
    }

    public virtual void OnInteract(PlayerScript player)
    {
        // Adjust player litter count
        player.AdjustLitter(litterType);

        //The player receives one point for each litter collected.(HS)
        GameManager.GetPlayerScript().AddLitter(this);
        GameManager.GetLitterManager().RemoveLitter(this);

        if(pickUpSound == null)
        {
            Debug.LogError(litterObj.name + " is missing audio clip");
        }

        GameManager.GetAudioManager().PlaySound(transform.position, pickUpSound);
        Destroy(gameObject);
    }

}
