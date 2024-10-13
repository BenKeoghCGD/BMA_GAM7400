/*
 * Branch: Main (Keogh, Ben)
 * Commit: c5c64a33b28ef4617eae3f6b5dcc3374872a0938
 * 
 * Cleaned 9/10/24 (Keogh, Ben)
 * Branch: Main, Stable (Keogh, Ben)
 * Commit: c5c64a33b28ef4617eae3f6b5dcc3374872a0938
 */

using UnityEngine;

public class Bin : MonoBehaviour, IInteractable
{
    [SerializeField] private LitterType litterType;
    private int _storedLitter = 0;

    public void OnInteract(PlayerScript player)
    {
        // Check if litter count is less than 10.
        if (_storedLitter >= 10) return;

        // increase stored amount by player holdage
        switch (litterType)
        {
            case LitterType.Beige:
                _storedLitter += player.HeldBeigeLitter;
                break;
            case LitterType.Red:
                _storedLitter += player.HeldRedLitter;
                break;
            case LitterType.Black:
                _storedLitter += player.HeldBlackLitter;
                break;
            default:
                _storedLitter += player.HeldBeigeLitter;
                break;
        }


        Debug.Log("Stored Litter: " + _storedLitter);

        // remove all litter of one type from plauyer
        player.ClearLitter(litterType);
        Debug.Log("CLEARED");
    }
}
