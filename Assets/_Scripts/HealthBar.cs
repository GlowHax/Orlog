using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public TMP_Text healthText;
    public Image healthBar;

    float health;
    int maxHealth = 15;

    private void Start()
    {
        health = maxHealth;
        UpdateVisuals();
    }

    void UpdateVisuals()
    {
        healthText.text = $"{health}/{maxHealth}";
        healthBar.fillAmount = health / maxHealth;
        Color healthColor = Color.Lerp(Color.red, Color.green, (health/maxHealth));
        healthBar.color = healthColor;
    }

    public void ChangeHealth(int difference) 
    {
        if(difference < 0 && health + difference < 0)
        {
            health = 0;
        }
        else if(difference > 0 && health + difference > maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += difference;
        }

        UpdateVisuals();
    }
}
