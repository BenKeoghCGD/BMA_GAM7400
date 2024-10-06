using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeigeBin : MonoBehaviour, IInteractable
{
    private int storedLitter = 0;

    public void OnInteract(PlayerController player)
    {
        if (storedLitter < 10)
        {
            storedLitter += player.HeldBeigeLitter;
            Debug.Log("Stored Litter: " + storedLitter);
            player.SetBeigeLitter(0);
        }
        else
        {
            return;
        }
    }
}
