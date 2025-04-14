using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndOfRoundView : View
{
    [SerializeField] private Button nextButton;
    [SerializeField] private TMP_Text titleText;


    [SerializeField] private TMP_Text player1NameText;
    [SerializeField] private HealthBar player1HealthBar;
    [SerializeField] private TMP_Text player1TokenCounterText;

    [SerializeField] private TMP_Text player2NameText;
    [SerializeField] private HealthBar player2HealthBar;
    [SerializeField] private TMP_Text player2TokenCounterText;

    private void Start()
    {
        GameManager.Instance.FavorsInResolvingOrder.Clear();
        UpdateVisuals();
    }

    public void StartNextRound()
    {
        GameManager.Instance.EndRound();
    }

    private void UpdateVisuals()
    {
        titleText.text = $"Final results of Round {GameManager.Instance.RoundCounter}";

        player1NameText.text = GameManager.Instance.Player1.Name;
        player2NameText.text = GameManager.Instance.Player2.Name;

        player1TokenCounterText.text = GameManager.Instance.Player1.FavorTokens.ToString();
        player2TokenCounterText.text = GameManager.Instance.Player2.FavorTokens.ToString();

        player1HealthBar.ChangeHealth(GameManager.Instance.Player1.Health - (int)player1HealthBar.Health);
        player2HealthBar.ChangeHealth(GameManager.Instance.Player2.Health - (int)player2HealthBar.Health);
    }
}