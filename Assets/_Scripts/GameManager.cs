/*
 * Branch: Hossein (Higham, Ben)
 * Commit: 057afddb335e248e8a0c986f6117e1e702cddf11
 * 
 * Cleaned 9/10/24 (Keogh, Ben)
 * Branch: Main, Stable (Keogh, Ben)
 * Commit: 
 */

// Updated 14/10/24 (Higham, Ben), changes commented

using PrimeTween;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    // Separated Litter management into separate class (BH)
    private LitterManager _litterManager;
    //Will be handled better in the future (BH)
    [SerializeField]
    private LitterDataList data;

    //instance of playerScript (HS)
    private PlayerScript _playerScript;
    private ScoreManager _scoreManager;
    
    public int PlayerScore = 0;
    public int StoredScore = 0;
    
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
            _litterManager = new LitterManager(data);
            _playerScript = FindObjectOfType<PlayerScript>();
            _scoreManager = FindObjectOfType<ScoreManager>();

        }

        Application.targetFrameRate = 60;
        PrimeTweenConfig.warnEndValueEqualsCurrent = false;

    }

    public static LitterManager GetLitterManager()
    {
        return instance._litterManager;
    }

    public static PlayerScript GetPlayerScript()
    {
        return instance._playerScript;
    }
    public static ScoreManager GetScoreManager()
    {
        return instance._scoreManager;
    }



}

/*public class GameManager : MonoBehaviour
{
   public static GameManager instance;
   public List<GameObject> LitterInstantiated = new List<GameObject>();
   
   private void Awake()
   {
      if (instance != null && instance != this) Destroy(gameObject);
      else instance = this;
   }
}*/