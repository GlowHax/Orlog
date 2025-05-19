using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OdinsSacrifice : FavorBehaviour
{
    public override void ResolveEffect(GodFavor godFavor)
    {
        Player owner = godFavor.GetOwner();
        FavorOption selectedOption = godFavor.selectedOption;
        if (owner.FavorTokens >= selectedOption.Cost)
        {
            UIManager.Instance.ShowView("OdinsSacrificeView");
            OdinsSacrificeView oSView = UIManager.Instance.CurrentView as OdinsSacrificeView;
            if (oSView != null)
            {
                owner.FavorTokens -= selectedOption.Cost;
                oSView.TitleText.text = $"Odin's Sacrifice ({owner.Name})";
                oSView._Slider.maxValue = owner.Health - 1;
                oSView._Slider.onValueChanged.AddListener((float value) =>
                {
                    oSView.FavorTokenCounterText.text = $"+{value * selectedOption.Value} favor tokens";
                });
                oSView.NextButton.onClick.AddListener(() =>
                {
                    owner.ChangeHealth(-(int)oSView._Slider.value);
                    owner.FavorTokens += (int)oSView._Slider.value * selectedOption.Value;
                });
            }
        }
        else
        {
            ShowNotEnoughFavorTokenView(godFavor);
        }
    }

}
