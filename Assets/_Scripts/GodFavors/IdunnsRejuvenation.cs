using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdunnsRejuvenation : FavorBehaviour
{
    public override void ResolveEffect(GodFavor godFavor)
    {
        Player owner = godFavor.owner;
        FavorOption selectedOption = godFavor.selectedOption;
        UIManager.Instance.ShowView("IdunnsRejuvenationView");
        IdunnsRejuvenationView iRView = UIManager.Instance.CurrentView as IdunnsRejuvenationView;
        if (iRView != null)
        {
            if (owner.FavorTokens >= selectedOption.Cost)
            {
                int previousHealth = owner.Health;
                owner.ChangeHealth(selectedOption.Value);
                iRView.EffectText.text = $"+{owner.Health - previousHealth} Health";
                owner.FavorTokens -= selectedOption.Cost;
            }
            else
            {
                ShowNotEnoughFavorTokenView(godFavor);
            }
        }
    }
}