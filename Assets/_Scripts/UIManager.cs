using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    private Dictionary<string, View> views;

    private void Awake()
    {
        LoadViews();
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void LoadViews()
    {
        views = Resources.LoadAll<View>("Views").ToDictionary(r => r.name, r => r);
    }

    private void GameManager_OnGameStateChanged(GameState state)
    {
        if(state == GameState.Starting)
        {
            ShowView(views.GetValueOrDefault("FavorSelectionMenu"));
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
