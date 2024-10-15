/*
 * Branch: Hossein (Higham, Ben)
 * Commit: 057afddb335e248e8a0c986f6117e1e702cddf11
 * 
 * Cleaned 9/10/24 (Keogh, Ben)
 * Branch: Main, Stable (Keogh, Ben)
 * Commit: 
 */

// Updated 14/10/24 (Higham, Ben), changes commented

using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    // Separated Litter management into separate class (BH)
    private LitterManager _litterManager;
    
    //instance of playerScript (HS)
    private PlayerScript _playerScript;
    
    
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

            //Creation will be handled outside of awake once menu scenes are added (BH)
            _litterManager = new LitterManager();
            _playerScript = new PlayerScript();
            
            
        }
    }

    public static LitterManager GetLitterManager()
    {
        return instance._litterManager;
    }

    public static PlayerScript GetPlayerScript()
    {
        return instance._playerScript;
    }



}

/*public class GameManager : MonoBehaviour
{
   public static GameManager instance;
   public int PlayerLife = 0;
   public List<GameObject> LitterInstantiated = new List<GameObject>();
   
   private void Awake()
   {
      if (instance != null && instance != this) Destroy(gameObject);
      else instance = this;
   }
}*/