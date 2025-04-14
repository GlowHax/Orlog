using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionPhaseView : View
{
    [SerializeField] private Button applyButton;
    [SerializeField] private Button nextButton;

    [SerializeField] private TMP_Text player1NameText;
    [SerializeField] private HealthBar player1HealthBar;
    [SerializeField] private TMP_Text player1TokenCounterText;
    [SerializeField] private Transform player1DiceResultsLayout;

    [SerializeField] private TMP_Text player2NameText;
    [SerializeField] private HealthBar player2HealthBar;
    [SerializeField] private TMP_Text player2TokenCounterText;
    [SerializeField] private Transform player2DiceResultsLayout;

    [SerializeField] private Die diePrefab;

    private void Start()
    {
        foreach (DieResult result in GameManager.Instance.Player1.PickedResults)
        {
            Die die = Instantiate(diePrefab.gameObject, player1DiceResultsLayout).GetComponent<Die>();
            die.ResultFace = result.face;
        }

        foreach (DieResult result in GameManager.Instance.Player2.PickedResults)
        {
            Die die = Instantiate(diePrefab.gameObject, player2DiceResultsLayout).GetComponent<Die>();
            die.ResultFace = result.face;
        }

        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        player1NameText.text = GameManager.Instance.Player1.Name;
        player2NameText.text = GameManager.Instance.Player2.Name;

        player1TokenCounterText.text = GameManager.Instance.Player1.FavorTokens.ToString();
        player2TokenCounterText.text = GameManager.Instance.Player2.FavorTokens.ToString();

        player1HealthBar.ChangeHealth(GameManager.Instance.Player1.Health - (int)player1HealthBar.Health);
        player2HealthBar.ChangeHealth(GameManager.Instance.Player2.Health - (int)player2HealthBar.Health);

        
    }

    public void Apply()
    {
        Player winner = GameManager.Instance.ApplyRoundResults();
        if (winner != null)
        {
            nextButton.onClick.RemoveAllListeners();
            nextButton.onClick.AddListener(() => UIManager.Instance.ShowText($"{winner.Name} WON THE GAME!"));
        }
        applyButton.interactable = false;
        nextButton.gameObject.SetActive(true);
        UpdateVisuals();
    }

    public void Next()
    {
        if(GameManager.Instance.FavorsInResolvingOrder.Count > 0)
        {
            GameManager.Instance.ResolveNextGodFavor();
        }
        else
        {
            GameManager.Instance.EndRound();
        }
    }
}