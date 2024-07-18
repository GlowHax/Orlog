using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

[Serializable]
public enum GameState
{
    MainMenu,
    Starting
}

public class GameManager : MonoBehaviour
{
    [field:SerializeField] public GameState State { get; private set; }

    public static event Action<GameState> OnGameStateChanged;

    [SerializeField] private UIManager _UIManager;

    public void EndGame()
    {
        ChangeState(GameState.MainMenu);
        _UIManager.ShowMainMenu();
    }

    public void StartGame()
    {
        ChangeState(GameState.Starting);
    }

    public void ChangeState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.MainMenu:
                break;
            case GameState.Starting:
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
