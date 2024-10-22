using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // reference to playerlife script
    [SerializeField] private PlayerLife PlayerLife;
    public bool isObstacle = true;


    // when player collides with the obstacles collider decreases the players health by 1, also ensures that the player health doesnt go under 0
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerLife == null) return;
       
            if (isObstacle == true)
            {
                PlayerLife.DecreasePlayerHealth();
            }
            else
            {
                PlayerLife.IncreasePlayerHealth();
            }


            PlayerLife.Health = Mathf.Max(PlayerLife.Health, 0);
        }
    }

   
}
