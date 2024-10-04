using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : MonoBehaviour, IInteractable
{
    private int storedLitter = 0;
    public void OnInteract(PlayerController player)
    {
        storedLitter += player.HeldLitter;
        Debug.Log("Stored Litter: " + storedLitter);

        player.SetLitter(0);
    }
}
