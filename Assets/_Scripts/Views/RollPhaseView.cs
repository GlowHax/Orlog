using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RollPhaseView : View
{
    [SerializeField] private Button rollButton;
    [SerializeField] private Button endTurnButton;
    [SerializeField] private TMP_Text roundsHeader;
    [SerializeField] private TMP_Text turnHeader;
    [SerializeField] private Transform diceResultsLayout;
    [SerializeField] private Transform selectedResultsLayout;
    [SerializeField] private Transform opponentsSelectedResultsLayout;
    [SerializeField] private HealthBar healthBarActivePlayer;
    [SerializeField] private HealthBar healthBarOtherPlayer;
    [SerializeField] private TMP_Text tokenCounterTextActivePlayer;
    [SerializeField] private TMP_Text tokenCounterTextOtherPlayer;

    [SerializeField] private Die diePrefab;

    private void Start()
    {
        InitEndTurnButton();
        RefreshView();
    }

    public void RefreshView()
    {
        InitRollButton();

        rollButton.gameObject.SetActive(true);
        endTurnButton.gameObject.SetActive(false);

        Player activePlayer = GameManager.Instance.PlayerOrder.First.Value;
        Player otherPlayer = GameManager.Instance.PlayerOrder.Last.Value;

        roundsHeader.text = "Round " + GameManager.Instance.RoundCounter;
        turnHeader.text = GameManager.Instance.PlayerOrder.First.Value.Name + 
            $"'s Turn ({GameManager.Instance.PlayerOrder.First.Value.TurnCounter}/3)";
        healthBarActivePlayer.ChangeHealth(activePlayer.Health - activePlayer.maxHealth);
        healthBarOtherPlayer.ChangeHealth(otherPlayer.Health - otherPlayer.maxHealth);
        tokenCounterTextActivePlayer.text = activePlayer.FavorTokens.ToString();
        tokenCounterTextOtherPlayer.text = otherPlayer.FavorTokens.ToString();

        ClearAllDiceLayouts();
        
        foreach(DieData data in activePlayer.RemainingDice)
        {
            Die die = Instantiate(diePrefab.gameObject, diceResultsLayout).GetComponent<Die>();
            die.Data = data;
            die.ResultFace = die.Data.Result;
            rollButton.onClick.AddListener(() => die.Roll());
        }

        foreach (DieResult result in activePlayer.PickedResults)
        {
            diePrefab.ResultFace = result.face;
            Instantiate(diePrefab.gameObject, selectedResultsLayout);
        }

        foreach (DieResult result in otherPlayer.PickedResults)
        {
            diePrefab.ResultFace = result.face;
            Instantiate(diePrefab.gameObject, opponentsSelectedResultsLayout);
        }
    }

    private void ClearAllDiceLayouts()
    {
        for (int i = 0; i < diceResultsLayout.childCount; i++)
        {
            Destroy(diceResultsLayout.GetChild(i).gameObject);
        }

        for (int i = 0; i < opponentsSelectedResultsLayout.childCount; i++)
        {
            Destroy(opponentsSelectedResultsLayout.GetChild(i).gameObject);
        }

        for (int i = 0; i < selectedResultsLayout.childCount; i++)
        {
            Destroy(selectedResultsLayout.GetChild(i).gameObject);
        }
    }

    private void InitRollButton()
    {
        rollButton.onClick.RemoveAllListeners();
        rollButton.onClick.AddListener(() => 
        { 
            rollButton.gameObject.SetActive(false);
            endTurnButton.gameObject.SetActive(true);
        });
    }
    
    private void InitEndTurnButton()
    {
        endTurnButton.onClick.RemoveAllListeners();
        endTurnButton.onClick.AddListener(() =>
        {
            GameManager.Instance.EndTurn();
            RefreshView();
        });
    }
}