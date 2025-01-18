using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : View
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button quitGameButton;
    private void Start()
    {
        startGameButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.FavorSelection));
        quitGameButton.onClick.AddListener(() => GameManager.Instance.QuitGame());
    }
}
