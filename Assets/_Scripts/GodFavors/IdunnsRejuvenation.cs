using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdunnsRejuvenation : FavorBehaviour
{
    public override void ResolveEffect(Player owner, FavorOption selectedOption)
    {
        UIManager.Instance.ShowView("IdunnsRejuvenationView");
        IdunnsRejuvenationView iRView = UIManager.Instance.CurrentView as IdunnsRejuvenationView;
        if (iRView != null)
        {
            if (owner.FavorTokens >= selectedOption.Cost)
            {
                int previousHealth = owner.Health;
                owner.ChangeHealth(selectedOption.Value);
                iRView.effectText.text = $"+{owner.Health - previousHealth} Health";
                owner.FavorTokens -= selectedOption.Cost;
            }
            else
            {
                iRView.effectText.text = $"Not enough favor tokens... ({owner.FavorTokens}/{selectedOption.Cost})";
            }
            iRView.titleText.text = $"Iðunn's Rejuvenation {owner.Name}";
        }
    }
}