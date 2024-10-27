using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    int health = 3;
    public int maxHealth = 3;

    public int Health { get => health; set => health = value; }

    public Image[] hearts;
    public Sprite FullHeart;
    public Sprite EmptyHeart;
    public void DecreasePlayerHealth()
    {
        health--;
        UpdatePlayerHealthUI();
    }

    public void IncreasePlayerHealth()
    {
        health++;
        UpdatePlayerHealthUI();
    }
    public void ResetHealth()
    {
        health = maxHealth;
        UpdatePlayerHealthUI();
    }

    void UpdatePlayerHealthUI()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = FullHeart;
            }
            else
            {
                hearts[i].sprite = EmptyHeart;
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
