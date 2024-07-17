using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

[Serializable]
public enum GameState
{
    Starting,
    Running,
    Pause
}

public class GameManager : MonoBehaviour
{
    [field:SerializeField] public GameState State { get; private set; }

    public static event Action<GameState> OnGameStateChanged;

    // Start is called before the first frame update
    public void StartGame()
    {
        ChangeState(GameState.Starting);
    }

    public void ChangeState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.Starting:
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
