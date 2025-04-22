using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OdinsSacrifice : FavorBehaviour
{
    public override void ResolveEffect(GodFavor godFavor)
    {
        Player owner = godFavor.owner;
        FavorOption selectedOption = godFavor.selectedOption;

        //UIManager.Instance.ShowView("OdinsSacrificeView");
        //OdinsSacrificeView oSView = UIManager.Instance.CurrentView as OdinsSacrificeView;
        //if (oSView != null)
        //{
        //    if (owner.FavorTokens >= selectedOption.Cost)
        //    {

        //        owner.FavorTokens -= selectedOption.Cost;
        //    }
        //    else
        //    {
        //        oSView.EffectText.text = $"Not enough favor tokens... ({owner.FavorTokens}/{selectedOption.Cost})";
        //    }
        //    oSView.TitleText.text = $"Odin's Sacrifice ({owner.Name})";
        //}
    }
}
