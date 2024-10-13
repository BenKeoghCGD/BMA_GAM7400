using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // reference to playerlife script
    [SerializeField] private PlayerLife PlayerLife;

    
    // when player collides with the obstacles collider decreases the players health by 1, also ensures that the player health doesnt go under 0
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerLife.health -= 1; 

            PlayerLife.health = Mathf.Max(PlayerLife.health, 0);
        }
    }
}
