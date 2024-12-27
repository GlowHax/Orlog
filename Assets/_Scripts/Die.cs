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
    [SerializeField] private Image frame;
    [SerializeField] private Button pickButton;
    [SerializeField] Face[] faces;

    public void SelectDie()
    {
        GameManager.Instance.activePlayer.PickedResults.Add(Result);
        frame.color = Color.green;

        pickButton.onClick.AddListener(DeselectDie);
        pickButton.onClick.RemoveListener(SelectDie);
    }

    public void DeselectDie()
    {
        GameManager.Instance.activePlayer.PickedResults.Remove(Result);
        frame.color = Color.black;

        pickButton.onClick.AddListener(SelectDie);
        pickButton.onClick.RemoveListener(DeselectDie);
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

        pickButton.onClick.RemoveListener(DeselectDie);
        pickButton.onClick.AddListener(SelectDie);
    }
}

[Serializable]
public class Face
{
    public string name;
    public Sprite sprite;
    public bool isGolden;
}