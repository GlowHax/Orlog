using System;
using System.Collections;
using System.Collections.Generic;

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
    public List<DieResult> PickedResults;
    public List<DieData> RemainingDice;


    public Player(string name)
    {
        Name = name;
    }

    public bool ChangeHealth(int difference)
    {
        if (difference < 0 && Health + difference < 0)
        {
            Health = 0;
            return true;
        }
        else if (difference > 0 && Health + difference > maxHealth)
        {
            Health = maxHealth;
            return false;
        }
        else
        {
            Health += difference;
            return false;
        }
    }
}
