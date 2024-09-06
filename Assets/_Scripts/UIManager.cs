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
                ShowView(views.GetValueOrDefault("MainMenu"));
                break;
            case GameState.FavorSelection:
                ShowView(views.GetValueOrDefault("FavorSelectionMenu"));
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

    private void ShowView(View view)
    {
        ClearUI();
        Instantiate(view.gameObject, canvas.transform);
    }
}
