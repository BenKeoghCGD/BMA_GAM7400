using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public int health = 3;
    public int maxHealth = 3;

    public Image[] hearts;
    public Sprite FullHeart;
  
    // Update is called once per frame
    void Update()
    {
        // health doesn't go over maximum
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        // loops through an array of sprites
        for (int i = 0; i < hearts.Length; i++)
        {
            // sets each heart to be either full or empty based on the current player health and sets the visibility based on current lives
            if (i < health)
            {
                hearts[i].sprite = FullHeart;
            }
            else
            {
                hearts[i].sprite = null;
            }
            if (i < maxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
