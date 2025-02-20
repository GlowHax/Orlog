using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdunnsRejuvenation : GodFavor
{
    public override void ResolveEffect()
    {
        if(owner.FavorTokens >= selectedOption.Cost)
        {
            owner.FavorTokens -= selectedOption.Cost;
            owner.Health += selectedOption.Value;
        }
        base.ResolveEffect();
    }
}