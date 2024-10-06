using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBin : MonoBehaviour, IInteractable
{
    private int storedLitter = 0;
    public void OnInteract(PlayerController player)
    {
        if (storedLitter < 3)
        {
            storedLitter += player.HeldBlackLitter;
            Debug.Log("Stored Litter: " + storedLitter);
            player.SetBlackLitter(0);
        }
        else
        {
            return;
        }
        
    }
}
