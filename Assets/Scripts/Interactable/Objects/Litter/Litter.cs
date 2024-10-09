/*
 * Branch: Main (Keogh, Ben)
 * Commit: 
 * 
 * Cleaned 9/10/24 (Keogh, Ben)
 * Branch: Main, Stable (Keogh, Ben)
 * Commit: 
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
