using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

[Serializable]
public enum GameState
{
    MainMenu,
    FavorSelection,
    Running,
    Pause
}

public class GameManager : StaticInstance<GameManager>
{
    [SerializeField] private GameState State;
    public static event Action<GameState> OnGameStateChanged;

    [SerializeField] private Player player1;
    [SerializeField] private Player player2;

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
            case GameState.Running:
                break;
            case GameState.Pause:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);

        }

        OnGameStateChanged?.Invoke(State);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
