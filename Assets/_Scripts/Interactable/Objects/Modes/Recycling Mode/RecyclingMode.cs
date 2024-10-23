using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecyclingMode : MonoBehaviour
{
    [SerializeField] private LitterSelector litterSelector;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (litterSelector != null)
            {
                litterSelector.OnMiniGameStart(other.GetComponent<PlayerScript>());
            }
            else
            {
                Debug.LogError("litter selector is not assigned in the inspector");
            }
            
        }

    }

}
