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

    [SerializeField] 
    public ToolType requiredTool;

    public ToolType RequiredTool => requiredTool;
    public bool hasrequiredTool;

    protected GameObject litterObj;

    protected int litterSize;
    public int LitterSize => litterSize;

   
    public void SetRequiredTool(ToolType tool)
    {
        requiredTool = tool;
    }

    public void Init(LitterData data, GameObject obj)
    {
        if (data == null)
        {
            Debug.LogError("Tried to create litter with null data.");
            return;
        }

        if (obj == null)
        {
            Debug.LogError("Object not assigned to litter");
            return;
        }

        litterObj = obj;

        if (data.litterName == null)
        {
            Debug.LogError("Litter missing name");
        }
        else
        {
            litterObj.name = data.litterName;
        }

        if (data.litterModel == null)
        {
            Debug.LogError("Litter missing model");
        }
        else
        {
            litterObj.AddComponent<MeshFilter>().mesh = data.litterModel;
        }

        if (data.litterMaterial == null)
        {
            Debug.LogError("Litter missing material");
        }
        else
        {
            litterObj.AddComponent<MeshRenderer>().material = data.litterMaterial;
        }

        if (data.litterSize == 0)
        {
            Debug.LogError("Litter has size 0");
        }
        else
        {
            litterSize = data.litterSize;
        }

        litterType = data.litterType;

        litterObj.AddComponent<BoxCollider>().isTrigger = true;
    }

    public virtual void OnInteract(PlayerScript player)
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
        
        hasrequiredTool = true;

        // Destroy itself, now removed from LitterManager
        GameManager.GetLitterManager().RemoveLitter(this);
        Destroy(gameObject);

    }
}
