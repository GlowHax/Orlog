using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UIManager : StaticInstance<UIManager>
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private TMP_Text notificationText;
    private Dictionary<string, View> views;
    public View CurrentView;

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
                ShowView("MainMenu");
                break;
            case GameState.FavorSelection:
                ShowView("FavorSelectionMenu");
                break;
            case GameState.RollPhase:
                ShowView("DiceRollView");
                break;
            case GameState.FavorPhase:
                ShowView("FavorPhaseView");
                break;
            case GameState.ResolutionPhase:
                ShowView("ResolutionPhaseView");
                break;
        }
    }

    public void ShowText(string content)
    {
        ClearUI();
        notificationText.text = content;
        Instantiate(notificationText.gameObject, canvas.transform);
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
        CurrentView = null;
    }

    public void ShowView(string viewName)
    {
        ClearUI();
        if (views.TryGetValue(viewName, out View view))
        {
            CurrentView = Instantiate(view.gameObject, canvas.transform).GetComponent<View>();
        }
    }
}
