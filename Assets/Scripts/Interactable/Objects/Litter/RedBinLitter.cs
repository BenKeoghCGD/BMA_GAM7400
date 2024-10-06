using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBinLitter : MonoBehaviour, IInteractable
{
    public void OnInteract(PlayerController player)
    {
        player.AdjustRedLitter(1);

        Destroy(gameObject);
    }
}
