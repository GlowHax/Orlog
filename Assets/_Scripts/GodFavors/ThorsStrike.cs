using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThorsStrike : FavorBehaviour
{
    public override void ResolveEffect(Player owner, FavorOption selectedOption)
    {
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
                        tSView.nextButton.onClick.RemoveAllListeners();
                        tSView.nextButton.onClick.AddListener(() => UIManager.Instance.ShowView("EndOfRoundView"));
                    }

                    tSView.effectText.text = $"{GameManager.Instance.Player2.Name}: -{selectedOption.Value} Health";
                }
                else
                {
                    if (GameManager.Instance.Player1.ChangeHealth(-selectedOption.Value))
                    {
                        tSView.nextButton.onClick.RemoveAllListeners();
                        tSView.nextButton.onClick.AddListener(() => UIManager.Instance.ShowView("EndOfRoundView"));
                    }

                    tSView.effectText.text = $"{GameManager.Instance.Player1.Name}: -{selectedOption.Value} Health";
                }
            }
            else
            {
                tSView.effectText.text = $"Not enough favor tokens... ({owner.FavorTokens}/{selectedOption.Cost})";
            }
            tSView.titleText.text = $"Thor's Strike ({owner.Name})";
        }
    }
}
