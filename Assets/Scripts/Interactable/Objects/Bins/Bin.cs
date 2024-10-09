/*
 * Branch: Main (Keogh, Ben)
 * Commit: 56f110d603535bc1d5ee8186f94c86515526ae0f
 * 
 * Cleaned 9/10/24 (Keogh, Ben)
 * Branch: Main, Stable (Keogh, Ben)
 * Commit: 
 */

using UnityEngine;

public class Bin : MonoBehaviour, IInteractable
{
    [SerializeField] private LitterType litterType;
    private int _storedLitter = 0;

    public void OnInteract(PlayerController player)
    {
        // Check if litter count is less than 10.
        if (_storedLitter >= 10) return;

        // increase stored amount by player holdage
        _storedLitter += player.HeldBeigeLitter;

        Debug.Log("Stored Litter: " + _storedLitter);

        // remove all litter of one type from plauyer
        player.ClearLitter(litterType);
    }
}
