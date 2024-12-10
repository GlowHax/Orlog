using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Die : MonoBehaviour
{
    public Face Result;
    public bool IsSelected;

    [SerializeField] private Image faceImg;
    [SerializeField] private Image goldenMarkImg;
    [SerializeField] private Button pickButton;
    [SerializeField] Face[] faces;

    public void SelectResult()
    {
        GameManager.Instance.activePlayer.PickedResults.Add(Result);

        pickButton.onClick.AddListener(DeselectResult);
        pickButton.onClick.RemoveListener(SelectResult);
    }

    public void DeselectResult()
    {
        GameManager.Instance.activePlayer.PickedResults.Remove(Result);

        pickButton.onClick.AddListener(SelectResult);
        pickButton.onClick.RemoveListener(DeselectResult);
    }

    public void Roll()
    {
        if(faces == null)
        {
            return;
        }

        Result = faces[UnityEngine.Random.Range(0, faces.Length)];

        faceImg.sprite = Result.sprite;
        if (Result.isGolden)
        {
            goldenMarkImg.enabled = true;
        }
        else
        {
            goldenMarkImg.enabled = false;
        }

        pickButton.onClick.RemoveListener(DeselectResult);
        pickButton.onClick.AddListener(SelectResult);
    }
}

[Serializable]
public class Face
{
    public string name;
    public Sprite sprite;
    public bool isGolden;
}