/*
 * Branch: Main (Keogh, Ben)
 * Commit: c5c64a33b28ef4617eae3f6b5dcc3374872a0938
 * 
 * Cleaned 9/10/24 (Keogh, Ben)
 * Branch: Main, Stable (Keogh, Ben)
 * Commit: c5c64a33b28ef4617eae3f6b5dcc3374872a0938
 */

using System;
using UnityEngine;

public class Bin : MonoBehaviour, IInteractable
{
    public LitterType litterType;
    [SerializeField] private int _BinCapasity = 10;
    
    [SerializeField] private int _storedBlackLitter = 0;
    [SerializeField] private int _storedBeigeLitter = 0;
    [SerializeField] private int _storedRedLitter = 0;

    [SerializeField] private bool _BinIsFull = false;

    public void OnInteract(PlayerScript player)
    {
        
        // increase stored amount by player holdage
        switch (litterType)
        {
            
            case LitterType.Beige:
                
                if (!_BinIsFull) //This checks if the bin is full, and if so, the rest of the code will not execute.(HS)
                {
                    if (player.HeldBeigeLitter >= _BinCapasity) //Here, it checks if the player is carrying as much or
                                                                //more trash than the bin’s capacity.
                                                                //It will deposit the allowed amount into the bin,
                                                                //and the remaining trash will stay with the player.(HS)
                    
                    {
                        _BinIsFull = true;
                        player.HeldBeigeLitter_Setter(player.HeldBeigeLitter - _BinCapasity);
                        _storedBeigeLitter += _BinCapasity;
                    }
                    else //Here, if the player's inventory amount is less than the bin's capacity,
                         //the same amount will be deposited into the bin, and the player's inventory will be set to zero.(HS)
                    {
                        _storedBeigeLitter += player.HeldBeigeLitter;
                        Debug.Log("Stored Litter: " + _storedBeigeLitter);
                        player.ClearLitter(litterType);
                    }
                }
                else
                {
                    Debug.Log("this bin is full");
                }
                break;
            case LitterType.Red:
                if (!_BinIsFull)
                {
                    if (player.HeldRedLitter >= _BinCapasity)
                    {
                        _BinIsFull = true;
                        player.HeldRedLitter_Setter(player.HeldRedLitter - _BinCapasity);
                        _storedRedLitter += _BinCapasity;
                    }
                    else
                    {
                        _storedRedLitter += player.HeldRedLitter;
                        Debug.Log("Stored Litter: " + _storedRedLitter);
                        player.ClearLitter(litterType);

                    }
                }
                else
                {
                    Debug.Log("this bin is full");
                }
                break;
            case LitterType.Black:
                if (!_BinIsFull)
                {
                    if (player.HeldBlackLitter >= _BinCapasity)
                    {
                        _BinIsFull = true;
                        player.HeldBlackLitter_Setter(player.HeldBlackLitter - _BinCapasity);
                        _storedBlackLitter += _BinCapasity;
                    }
                    else
                    {
                        _storedBlackLitter += player.HeldBlackLitter;
                        Debug.Log("Stored Litter: " + _storedBlackLitter);
                        player.ClearLitter(litterType);

                    }
                }
                else
                {
                    Debug.Log("this bin is full");
                }
                break;
        }
        

    }


    public int GetStoredBlackLitter
    { 
        get { return _storedBlackLitter; }
    }

    //Here, we could only retrieve the values held by the trash bins,
    //but we also need the ability to modify these values. Therefore, I added setters to enable this functionality.(HS)
    public void SetStoredBlackLitter(int value)
    {
        _storedBlackLitter = value;
    }
    public int GetStoredBeigeLitter
    {
        get { return _storedBeigeLitter; }
    }
    public void SetStoredBeigeLitter(int value)
    {
        _storedBeigeLitter = value;
    }
    public int GetStoredRedLitter
    {
        get { return _storedRedLitter; }
    }
    public void SetStoredRedLitter(int value)
    {
        _storedRedLitter = value;
    }
    /*
    public void OnInteract(PlayerScript player)
    {
        // Check if litter count is less than 10.
        if (_storedLitter >= 10)
        {
            return;
        }

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
    */
}
