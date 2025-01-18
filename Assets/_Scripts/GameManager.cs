using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum GameState
{
    MainMenu,
    FavorSelection,
    Starting,
    RollPhase,
    FavorPhase,
    ResolutionPhase,
    EndOfRound,
    Pause
}

public class GameManager : StaticInstance<GameManager>
{
    [SerializeField] private GameState State;
    public static event Action<GameState> OnGameStateChanged;

    public Player Player1;
    public Player Player2;

    public int RoundCounter = 1;
    public LinkedList<Player> PlayerOrder = new LinkedList<Player>();

    public DieData[] DiceData = new DieData[6];

    private void Start()
    {
        ChangeState(GameState.MainMenu);
    }

    public void ChangeState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.MainMenu:
                break;
            case GameState.FavorSelection:
                Player1 = new Player("Player 1");
                Player2 = new Player("Player 2");
                break;
            case GameState.Starting:
                RoundCounter = 1;
                PlayerOrder.Clear();
                if (Convert.ToBoolean(new System.Random().Next(2)))
                {
                    PlayerOrder.AddFirst(Player2);
                    PlayerOrder.AddLast(Player1);
                }
                else
                {
                    PlayerOrder.AddFirst(Player1);
                    PlayerOrder.AddLast(Player2);
                }
                ChangeState(GameState.RollPhase);
                break;
            case GameState.RollPhase:
                break;
            case GameState.FavorPhase:
                Player1.TurnCounter = 1;
                Player2.TurnCounter = 1;
                break;
            case GameState.ResolutionPhase:
                foreach (Player player in PlayerOrder)
                {
                    
                }
                break;
            case GameState.EndOfRound:
                break;
            case GameState.Pause:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);

        }

        OnGameStateChanged?.Invoke(State);
    }

    public void EndTurn()
    {
        bool activePlayerNoPicksLeft = true;
        bool nextPlayerNoPicksLeft = true;
        for(int i = 0; i < 6; i++)
        {
            if (PlayerOrder.First.Value.PickedResults[i] == null)
            {
                activePlayerNoPicksLeft = false;
            }
            if (PlayerOrder.Last.Value.PickedResults[i] == null)
            {
                nextPlayerNoPicksLeft = false;
            }
        }
        if(activePlayerNoPicksLeft && nextPlayerNoPicksLeft)
        {
            ChangeState(GameState.FavorPhase);
        }
        else if(nextPlayerNoPicksLeft)
        {
            PlayerOrder.First.Value.TurnCounter++;
        }
        else
        {
            PlayerOrder.First.Value.TurnCounter++;
            PlayerOrder.AddLast(PlayerOrder.First.Value);
            PlayerOrder.RemoveFirst();
        }
    }

    public void EndRound()
    {
        RoundCounter++;
        
        Player1.PickedResults = new DieResult[6];
        Player2.PickedResults = new DieResult[6];
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

public class DieResult
{
    public Face face;

    public DieResult(Face face)
    {
        this.face = face;
    }
}