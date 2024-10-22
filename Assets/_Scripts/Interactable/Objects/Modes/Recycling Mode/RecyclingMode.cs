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
            litterSelector.OnMiniGameStart(other.GetComponent<PlayerScript>());
        }

    }

}
