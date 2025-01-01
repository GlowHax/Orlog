using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[Serializable]
public enum GameState
{
    MainMenu,
    FavorSelection,
    Starting,
    Running,
    Pause
}

public class GameManager : StaticInstance<GameManager>
{
    [SerializeField] private GameState State;
    public static event Action<GameState> OnGameStateChanged;

    public Player Player1;
    public Player Player2;

    public int RoundCounter = 1;
    public Queue<Player> PlayerQueue = new Queue<Player>();

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
                PlayerQueue.Clear();
                if (Convert.ToBoolean(new System.Random().Next(2)))
                {
                    PlayerQueue.Enqueue(Player2);
                    PlayerQueue.Enqueue(Player1);
                }
                else
                {
                    PlayerQueue.Enqueue(Player1);
                    PlayerQueue.Enqueue(Player2);
                }
                break;
            case GameState.Running:

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
            if (PlayerQueue.Peek().PickedResults[i] == null)
            {
                activePlayerNoPicksLeft = false;
            }
            if (PlayerQueue.Last().PickedResults[i] == null)
            {
                nextPlayerNoPicksLeft = false;
            }
        }
        if(activePlayerNoPicksLeft && nextPlayerNoPicksLeft)
        {
            EndRound();
        }
        else if(nextPlayerNoPicksLeft)
        {
            PlayerQueue.Peek().TurnCounter++;
        }
        else
        {
            PlayerQueue.Peek().TurnCounter++;
            PlayerQueue.Enqueue(PlayerQueue.Dequeue());
        }
    }

    public void EndRound()
    {
        RoundCounter++;

        Player1.TurnCounter = 1; 
        Player2.TurnCounter = 1;
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
    public int ID;
    public Face face;

    public DieResult(int id, Face face)
    {
        ID = id;
        this.face = face;
    }
}