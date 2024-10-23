/*
 * Branch: BenH (Higham, Ben)
 * Commit: 56f110d603535bc1d5ee8186f94c86515526ae0f
 * 
 * Branch: Hossein (Soroor, Hossein)
 * Commit: 057afddb335e248e8a0c986f6117e1e702cddf11
 * 
 * Cleaned 9/10/24 (Keogh, Ben)
 * Branch: Main, Stable (Keogh, Ben)
 * Commit: c5c64a33b28ef4617eae3f6b5dcc3374872a0938, b19f37c3e27817dba491819664ea3e95d924333e
 */

using System.Collections.Generic;
using UnityEngine;


public class LitterDropper : MonoBehaviour
{
    [SerializeField] Litter litter;

   private ToolType getMatchingTool(LitterType litterType)
    {
        switch (litterType)
        {
            case LitterType.Spillage:
                return ToolType.Mop;
            case LitterType.CansBottles:
                return ToolType.LitterPicker;
            case LitterType.GeneralWaste:
                return ToolType.Brush;
            case LitterType.Cardboard:
                return ToolType.Gloves;
            case LitterType.FoodGarden:
                return ToolType.Gloves;
            
            default:
                return ToolType.LitterPicker;
        }
    }

    // Function to spawn litter
    public void DropLitter()
    {
        if (litter == null)
        {
            Debug.LogWarning("litter is not assigned in litter dropper");
            return;
        }
        //Creates a new Litter object
        GameObject litterObject = new GameObject();

        Litter instance = litterObject.AddComponent<Litter>();
        instance.gameObject.layer = LayerMask.NameToLayer("Interactable");
        instance.transform.position = new Vector3(transform.position.x, 1, transform.position.z);

        instance.Init(GameManager.GetLitterManager().GetRandomLitterData(), litterObject);
        
        LitterType randomLitterType = GetRandomLitterType();
        litter.SetLitterType(randomLitterType);

        ToolType matchingToolType = getMatchingTool(randomLitterType);
        litter.SetRequiredTool(matchingToolType);

        // Adds the litter item to the LitterManager
       // GameManager.GetLitterManager().AddLitter(instance.GetComponent<Litter>());
    }

    private LitterType GetRandomLitterType()
    {
        // Get litter types from the LitterType enum
        LitterType[] litterTypes = (LitterType[])System.Enum.GetValues(typeof(LitterType));
        return litterTypes[Random.Range(3, litterTypes.Length)];
    }
    // Set the litterTimer variable to a random number between litterTimerMin and litterTimerMax
}
