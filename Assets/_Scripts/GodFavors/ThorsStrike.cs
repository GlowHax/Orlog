using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThorsStrike : FavorBehaviour
{
    public override void ResolveEffect(Player owner, FavorOption selectedOption)
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
