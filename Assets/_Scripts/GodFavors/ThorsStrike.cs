using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThorsStrike : GodFavor
{
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
        base.ResolveEffect();
    }
}
