using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoryView : View
{
    [SerializeField] private TMP_Text victoryText;

    public void ShowWinner(string winnerName)
    {
        victoryText.text = $"{winnerName} WON THE GAME!";
    }
}
