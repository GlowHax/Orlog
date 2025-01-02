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

    [SerializeField] private Die diePrefab;

    private void Start()
    {
        endTurnButton.gameObject.SetActive(false);
        rollButton.onClick.AddListener(() => rollButton.gameObject.SetActive(false));
        rollButton.onClick.AddListener(() => endTurnButton.gameObject.SetActive(true));
        endTurnButton.onClick.AddListener(() => GameManager.Instance.EndTurn());
        endTurnButton.onClick.AddListener(() => RefreshView());
        roundsHeader.text = "Round " + GameManager.Instance.RoundCounter;
        turnHeader.text = GameManager.Instance.PlayerQueue.Peek().Name + 
            $"'s Turn ({GameManager.Instance.PlayerQueue.Peek().TurnCounter}/3)";
        RefreshView();
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

        for(int i = 0; i < selectedResultsLayout.childCount; i++)
        {
            Destroy(selectedResultsLayout.GetChild(i).gameObject);
        }

        for(int i = 0; i < activePlayer.PickedResults.Length; i++)
        {
            if (activePlayer.PickedResults[i] == null)
            {
                Die die = Instantiate(diePrefab.gameObject, diceResultsLayout).GetComponent<Die>();
                die.Data = GameManager.Instance.DiceData[i];
                die.ResultFace = die.Data.Result;
                rollButton.onClick.AddListener(() => die.Roll());
            }
            else
            {
                diePrefab.ResultFace = activePlayer.PickedResults[i].face;
                Instantiate(diePrefab.gameObject, selectedResultsLayout);
            }
        }

        foreach (DieResult result in otherPlayer.PickedResults)
        {
            if(result != null)
            {
                diePrefab.ResultFace = result.face;
                Instantiate(diePrefab.gameObject, opponentsSelectedResultsLayout);
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