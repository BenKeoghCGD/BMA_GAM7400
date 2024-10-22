/*
 * Branch: Main (Keogh, Ben)
 * Commit: c5c64a33b28ef4617eae3f6b5dcc3374872a0938
 * 
 * Cleaned 9/10/24 (Keogh, Ben)
 * Branch: Main, Stable (Keogh, Ben)
 * Commit: c5c64a33b28ef4617eae3f6b5dcc3374872a0938
 */

// Updated 14/10/24 (Higham, Ben), changes commented

using UnityEngine;

public class Litter : MonoBehaviour, IInteractable
{
    [SerializeField] 
    public LitterType litterType;
    [SerializeField] public ToolType requiredTool;

    public ToolType RequiredTool => requiredTool;
 

    void Start()
    {
        GameManager.GetLitterManager().AddLitter(this);
        
    }

   
    public void SetRequiredTool(ToolType tool)
    {
        requiredTool = tool;
    }

    public void SetLitterType(LitterType Type)
    {
        litterType = Type;
    }    
    public void OnInteract(PlayerScript player)
    {
        if (!player.hasRequiredTool(requiredTool))
        {
            Debug.Log("Incorrect Tool");
            return;
        }
        // Adjust player litter count
        player.AdjustLitter(litterType);

        //The player receives one point for each litter collected.(HS)
        GameManager.GetPlayerScript().CalculateCollectedLitter(false,1);
        
        

        // Destroy itself, now removed from LitterManager
        GameManager.GetLitterManager().RemoveLitter(this);
        Destroy(gameObject);

    }
}
