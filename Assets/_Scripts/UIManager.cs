using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : StaticInstance<UIManager>
{
    [SerializeField] private Canvas canvas;
    private Dictionary<string, View> views;

    protected override void Awake()
    {
        base.Awake();
        LoadViews();
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void LoadViews()
    {
        views = Resources.LoadAll<View>("Views").ToDictionary(r => r.name, r => r);
    }

    private void GameManager_OnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.MainMenu:
                if(views.TryGetValue("MainMenu", out View mainMenu))
                {
                    ShowView(mainMenu);
                }
                break;
            case GameState.FavorSelection:
                if(views.TryGetValue("FavorSelectionMenu", out View favorSelectionMenu))
                {
                    ShowView(favorSelectionMenu);
                }
                break;
            case GameState.RollPhase:
                if (views.TryGetValue("DiceRollView", out View diceRollView))
                {
                    ShowView(diceRollView);
                }
                break;
            case GameState.FavorOptionSelection:
                if (views.TryGetValue("FavorPhaseView", out View favorPhaseView))
                {
                    ShowView(favorPhaseView);
                }
                break;
            case GameState.ResolutionPhase:
                if (views.TryGetValue("ResolutionPhaseView", out View resolutionPhaseView))
                {
                    ShowView(resolutionPhaseView);
                }
                break;
        }
    }

    private void ClearUI()
    {
        List<GameObject> childObjects = new List<GameObject>();
        for(int i = 0; i < canvas.transform.childCount; i++)
        {
            childObjects.Add(canvas.transform.GetChild(i).gameObject);
        }

        foreach(GameObject obj in childObjects) 
        {
            Destroy(obj);
        }
    }

    public void ShowView(View view)
    {
        ClearUI();
        Instantiate(view.gameObject, canvas.transform);
    }
}
