using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : View
{
    [SerializeField] private Button rollButton;
    [SerializeField] private TMP_Text roundsHeader;
    [SerializeField] private TMP_Text turnHeader;
    [SerializeField] private Transform diceResultsLayout;
    [SerializeField] private Transform SelectedResultsLayout;
    [SerializeField] private Die[] dice;

    private void Start()
    {
        GameManager.Instance.currentDice = dice;

        rollButton.onClick.AddListener(() => GameManager.Instance.RollDice());
        roundsHeader.text = "Round " + GameManager.Instance.RoundCounter;
        turnHeader.text = GameManager.Instance.activePlayer.Name + $"'s Turn ({GameManager.Instance.TurnCounter}/3)";
    }

    public void UpdateSelectionSection()
    {

    }
}
