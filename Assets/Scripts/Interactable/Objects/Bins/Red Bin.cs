using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBin : MonoBehaviour, IInteractable
{
    private int storedLitter = 0;

    public void OnInteract(PlayerController player)
    {
        if (storedLitter < 10)
        {
            storedLitter += player.HeldRedLitter;
            Debug.Log("Stored Litter: " + storedLitter);
            player.SetRedLitter(0);
        }
        else
        {
            return;
        }
    }
}
