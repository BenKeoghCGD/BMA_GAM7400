using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecyclingMode : MonoBehaviour
{
    [SerializeField] private RecyclingManager recyclingManager;


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            PlayerScript playerScript = other.GetComponent<PlayerScript>();
           // if (playerScript.litterCollectedAmount > 0)
           // {
                if (recyclingManager != null)
                {
                    recyclingManager.OnMiniGameStart(playerScript);
                }
                else
                {
                    Debug.LogError("litter selector is not assigned in the inspector");
                }
          //  }

        }

    }

}
