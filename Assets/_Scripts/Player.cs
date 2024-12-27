using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player
{
    public string Name;
    public int Health = 15;
    public int FavorTokens;
    public Godfavor[] Godfavors = new Godfavor[3];
    public List<Face> PickedResults = new List<Face>();

    public Player(string name)
    {
        Name = name;
    }
}
