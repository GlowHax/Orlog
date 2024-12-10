using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    public Player player1;
    public Player player2;

    public int RoundCounter = 1;
    public int TurnCounter = 1;
    public Player activePlayer;

    public Die[] currentDice;

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
                player1 = new Player();
                player2 = new Player();
                break;
            case GameState.Starting:
                if (Convert.ToBoolean(new System.Random().Next(2)))
                {
                    activePlayer = player2;
                }
                else
                {
                    activePlayer = player1;
                }
                TurnCounter = 1;
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

    public void RollDice()
    {
        foreach (Die die in currentDice) 
        {
            die.Roll();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}