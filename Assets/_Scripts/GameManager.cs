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
    private AI_SpawnManager _AISpawnManager;
    //Below will be handled better in the future (BH)
    [SerializeField]
    private LitterDataList data;
    [SerializeField]
    private GameObject customerPrefab;
    [SerializeField]
    private GameObject pedestrianPrefab;
    [SerializeField]
    private GameObject carPrefab;

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
            _AISpawnManager = new AI_SpawnManager(customerPrefab, pedestrianPrefab, carPrefab);
            _playerScript = FindObjectOfType<PlayerScript>();
            _scoreManager = FindObjectOfType<ScoreManager>();
        }
    }

    private void Update()
    {
        if(_AISpawnManager == null)
        {
            Debug.LogError("Missing AI_SpawnManager Reference in GameManager");
        }

        instance._AISpawnManager.Update(Time.deltaTime);
    }
    public static LitterManager GetLitterManager()
    {
        return instance._litterManager;
    }
    public static AI_SpawnManager GetAISpawnManager()
    {
        return instance._AISpawnManager;
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