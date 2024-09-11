using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player
{
    public int Health = 15;
    public int FavorTokens;
    public Godfavor[] Godfavors = new Godfavor[3];
    public bool isStartingPlayer;
}
