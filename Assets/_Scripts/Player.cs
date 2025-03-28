using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player
{
    public string Name;
    public int Health = 15;
    public int maxHealth = 15;
    public int FavorTokens;
    public int TurnCounter = 1;
    public GodFavor[] Godfavors = new GodFavor[3];
    public GodFavor selectedGodfavor;
    public DieResult[] PickedResults = new DieResult[6];

    public Player(string name)
    {
        Name = name;
    }

    public void ChangeHealth(int difference)
    {
        if (difference < 0 && Health + difference < 0)
        {
            Health = 0;
        }
        else if (difference > 0 && Health + difference > maxHealth)
        {
            Health = maxHealth;
        }
        else
        {
            Health += difference;
        }
    }
}
