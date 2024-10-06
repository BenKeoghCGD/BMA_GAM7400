using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeigeBinLitter : MonoBehaviour, IInteractable
{
    public void OnInteract(PlayerController player)
    {
        player.AdjustBeigeLitter(1);

        Destroy(gameObject);
    }
}
