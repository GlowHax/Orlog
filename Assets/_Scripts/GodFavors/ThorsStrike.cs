using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThorsStrike : GodFavor
{
    private void Awake()
    {
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(GameState state)
    {
        if (state == GameState.EndOfRound)
        {
            ResolveEffect();
        }
    }

    public override void ResolveEffect()
    {
        if (owner.FavorTokens >= selectedOption.Cost)
        {
            owner.FavorTokens -= selectedOption.Cost;
            if (owner == GameManager.Instance.Player1)
            {
                GameManager.Instance.Player2.Health -= selectedOption.Value;
            }
            else
            {
                GameManager.Instance.Player1.Health -= selectedOption.Value;
            }
        }
    }
}
