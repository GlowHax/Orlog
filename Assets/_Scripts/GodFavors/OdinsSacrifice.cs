using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OdinsSacrifice : FavorBehaviour
{
    public override void ResolveEffect(Player owner, FavorOption selectedOption)
    {
        if (owner.FavorTokens >= selectedOption.Cost)
        {
            owner.FavorTokens -= selectedOption.Cost;

        }
    }
}
