using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public TMP_Text healthText;
    public Image healthBar;

    public float Health;
    int maxHealth = 15;

    private void Awake()
    {
        Health = maxHealth;
    }

    private void Start()
    {
        UpdateVisuals();
    }

    void UpdateVisuals()
    {
        healthText.text = $"{Health}/{maxHealth}";
        healthBar.fillAmount = Health / maxHealth;
        Color healthColor = Color.Lerp(Color.red, new Color(0, 0.7f, 0), (Health/maxHealth));
        healthBar.color = healthColor;
    }

    public void ChangeHealth(int difference) 
    {
        if(difference < 0 && Health + difference < 0)
        {
            Health = 0;
        }
        else if(difference > 0 && Health + difference > maxHealth)
        {
            Health = maxHealth;
        }
        else
        {
            Health += difference;
        }

        UpdateVisuals();
    }
}
