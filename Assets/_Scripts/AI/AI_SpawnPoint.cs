using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private SpawnPointType spawnPointType;

    public bool isUsed = false;

    void Start()
    {
        GameManager.GetAISpawnManager().AddSpawnPoint(this, spawnPointType);
    }

    public void SpawnAgent(GameObject prefab)
    {
        if(spawnPointType == SpawnPointType.CUSTOMER && isUsed == true)
        {
            Debug.LogError("SpawnPoint in use, check logic.");
            return;
        }

        GameObject agent = Instantiate(prefab, transform.position, Quaternion.identity);
        agent.GetComponent<Agent_Base>().SetSpawnPoint(this);

        isUsed = true;
    }
}
