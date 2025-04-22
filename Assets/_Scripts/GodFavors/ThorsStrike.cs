using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThorsStrike : FavorBehaviour
{
    public override void ResolveEffect(GodFavor godFavor)
    {
        Player owner = godFavor.owner;
        FavorOption selectedOption = godFavor.selectedOption;
        UIManager.Instance.ShowView("ThorsStrikeView");
        ThorsStrikeView tSView = UIManager.Instance.CurrentView as ThorsStrikeView;
        if (tSView != null)
        {
            if (owner.FavorTokens >= selectedOption.Cost)
            {
                owner.FavorTokens -= selectedOption.Cost;
                if (owner == GameManager.Instance.Player1)
                {
                    if (GameManager.Instance.Player2.ChangeHealth(-selectedOption.Value))
                    {
                        tSView.NextButton.onClick.RemoveAllListeners();
                        tSView.NextButton.onClick.AddListener(() => UIManager.Instance.ShowView("EndOfRoundView"));
                    }

                    tSView.EffectText.text = $"{GameManager.Instance.Player2.Name}: -{selectedOption.Value} Health";
                }
                else
                {
                    if (GameManager.Instance.Player1.ChangeHealth(-selectedOption.Value))
                    {
                        tSView.NextButton.onClick.RemoveAllListeners();
                        tSView.NextButton.onClick.AddListener(() => UIManager.Instance.ShowView("EndOfRoundView"));
                    }

                    tSView.EffectText.text = $"{GameManager.Instance.Player1.Name}: -{selectedOption.Value} Health";
                }
            }
            else
            {
                ShowNotEnoughFavorTokenView(godFavor);
            }
            tSView.TitleText.text = $"Thor's Strike ({owner.Name})";
        }
    }
}
