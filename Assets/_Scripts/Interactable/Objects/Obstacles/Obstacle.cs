using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public bool isObstacle = true;
    private AudioSource hitbytrolley;

    private void Start()
    {
        hitbytrolley = GetComponent<AudioSource>();
    }
    // This will be called when the obstacle collides with something
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Get the PlayerLife component from the colliding object (an instance)
            PlayerLife playerlife = other.GetComponent<PlayerLife>();

            if (playerlife != null)  // Ensure playerlife is found
            {
                if (isObstacle)
                {
                    playerlife.DecreasePlayerHealth();  // Decrease health
                }
                else
                {
                    playerlife.IncreasePlayerHealth();  // Increase health
                }

                // Ensure health doesn't go below 0
                playerlife.Health = Mathf.Max(playerlife.Health, 0);
            }
           
            if (CompareTag("NPC") && hitbytrolley != null)
            {
                hitbytrolley.Play();
            }
        }
    }
}
