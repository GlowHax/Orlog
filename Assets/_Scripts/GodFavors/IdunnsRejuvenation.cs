using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdunnsRejuvenation : GodFavor
{
    private void Awake()
    {
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(GameState state)
    {
        if(state == GameState.EndOfRound)
        {
            ResolveEffect();
        }
    }

    public override void ResolveEffect()
    {
        if(owner.FavorTokens >= selectedOption.Cost)
        {
            owner.FavorTokens -= selectedOption.Cost;
            owner.Health += selectedOption.Value;
        }
    }
}