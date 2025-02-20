using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OdinsSacrifice : GodFavor
{
    public override void ResolveEffect()
    {
        if (owner.FavorTokens >= selectedOption.Cost)
        {
            owner.FavorTokens -= selectedOption.Cost;
            UIManager.Instance.ShowView(extraView);
        }
        base.ResolveEffect();
    }
}
