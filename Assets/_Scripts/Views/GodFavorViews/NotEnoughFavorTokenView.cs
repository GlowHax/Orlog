using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotEnoughFavorTokenView : View
{
    public TMP_Text TitleText;
    public TMP_Text NotificationText;
    public Button NextButton;

    public void Init(GodFavor godFavor)
    {
        TitleText.text = $"{godFavor.Name} ({godFavor.owner.Name})";
        NotificationText.text =
        $"Not enough favor tokens... ({godFavor.owner.FavorTokens}/{godFavor.selectedOption.Cost})";
        NextButton.onClick.AddListener(() => GameManager.Instance.ResolveNextGodFavor());
    }
}
