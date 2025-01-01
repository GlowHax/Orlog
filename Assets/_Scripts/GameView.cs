using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : View
{
    [SerializeField] private Button rollButton;
    [SerializeField] private Button endTurnButton;
    [SerializeField] private TMP_Text roundsHeader;
    [SerializeField] private TMP_Text turnHeader;
    [SerializeField] private Transform diceResultsLayout;
    [SerializeField] private Transform selectedResultsLayout;
    [SerializeField] private Transform opponentsSelectedResultsLayout;

    [SerializeField] private Transform diePrefab;

    private void Start()
    {
        rollButton.onClick.AddListener(() => rollButton.gameObject.SetActive(false));
        rollButton.onClick.AddListener(() => endTurnButton.gameObject.SetActive(true));
        endTurnButton.onClick.AddListener(() => GameManager.Instance.EndTurn());
        endTurnButton.onClick.AddListener(() => RefreshView());
        roundsHeader.text = "Round " + GameManager.Instance.RoundCounter;
        turnHeader.text = GameManager.Instance.PlayerQueue.Peek().Name + 
            $"'s Turn ({GameManager.Instance.PlayerQueue.Peek().TurnCounter}/3)";
        for(int i = 0; i < 6; i++)
        {
            Die die = Instantiate(diePrefab, diceResultsLayout).GetComponent<Die>();
            die.Data = GameManager.Instance.DiceData[i];
            die.ResultFace = die.Data.Result;
            rollButton.onClick.AddListener(() => die.Roll());
        }
    }

    public void RefreshView()
    {
        InitRollButton();
        roundsHeader.text = "Round " + GameManager.Instance.RoundCounter;
        turnHeader.text = GameManager.Instance.PlayerQueue.Peek().Name + 
            $"'s Turn ({GameManager.Instance.PlayerQueue.Peek().TurnCounter}/3)";

        Player activePlayer = GameManager.Instance.PlayerQueue.Peek();
        Player otherPlayer = GameManager.Instance.PlayerQueue.Last();

        for (int i = 0; i < diceResultsLayout.childCount; i++)
        {
            Destroy(diceResultsLayout.GetChild(i).gameObject);
        }

        for (int i = 0; i < opponentsSelectedResultsLayout.childCount; i++)
        {
            Destroy(opponentsSelectedResultsLayout.GetChild(i).gameObject);
        }

        for(int i = 0; i < activePlayer.PickedResults.Length; i++)
        {
            if (activePlayer.PickedResults[i] == null)
            {
                Die die = Instantiate(diePrefab, diceResultsLayout).GetComponent<Die>();
                die.Data = GameManager.Instance.DiceData[i];
                die.ResultFace = die.Data.Result;
                rollButton.onClick.AddListener(() => die.Roll());
            }
        }

        foreach (DieResult result in otherPlayer.PickedResults)
        {
            if(result != null)
            {
                diePrefab.GetComponent<Die>().ResultFace = result.face;
                Instantiate(diePrefab, opponentsSelectedResultsLayout).GetComponent<Die>();
            }
        }
    }

    public void InitRollButton()
    {
        rollButton.onClick.RemoveAllListeners();
        rollButton.onClick.AddListener(() => 
        { 
            rollButton.gameObject.SetActive(false);
            endTurnButton.gameObject.SetActive(true);
        });
        rollButton.gameObject.SetActive(true);
        endTurnButton.gameObject.SetActive(false);
    }
}