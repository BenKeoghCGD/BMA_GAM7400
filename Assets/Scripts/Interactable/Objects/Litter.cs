using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Litter : MonoBehaviour, IInteractable
{
    public void OnInteract(PlayerController player)
    {
        player.AdjustLitter(1);

        Destroy(gameObject);
    }
}
