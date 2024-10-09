/*
 * Branch: Main (Keogh, Ben)
 * Commit: c5c64a33b28ef4617eae3f6b5dcc3374872a0938
 * 
 * Cleaned 9/10/24 (Keogh, Ben)
 * Branch: Main, Stable (Keogh, Ben)
 * Commit: c5c64a33b28ef4617eae3f6b5dcc3374872a0938
 */

using UnityEngine;

public class Litter : MonoBehaviour, IInteractable
{
    [SerializeField] private LitterType litterType;

    public void OnInteract(PlayerController player)
    {
        // Adjust player litter count
        player.AdjustLitter(litterType);

        // Destroy itself
        Destroy(gameObject);
    }
}
