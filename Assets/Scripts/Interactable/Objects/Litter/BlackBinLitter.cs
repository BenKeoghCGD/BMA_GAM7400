using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBinLitter : MonoBehaviour, IInteractable
{
    public void OnInteract(PlayerController player)
    {
        player.AdjustBlackLitter(1);
        
        Destroy(gameObject);
    }
}
