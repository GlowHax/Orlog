using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Die : MonoBehaviour
{
    public Face ResultFace;
    public DieData Data;

    [SerializeField] private Image faceImg;
    [SerializeField] private Image goldenMarkImg;
    [SerializeField] private Image frame;
    [SerializeField] private Button selectButton;

    private void Start()
    {
        if(ResultFace != null)
        {
            faceImg.sprite = ResultFace.sprite;

            if (ResultFace.isGolden)
            {
                goldenMarkImg.gameObject.SetActive(true);
            }
            else
            {
                goldenMarkImg.gameObject.SetActive(false);
            }
        }
    }

    public void Select()
    {
        GameManager.Instance.PlayerOrder.First.Value.PickedResults[Data.ID - 1] = new DieResult(ResultFace);
        frame.color = Color.green;

        selectButton.onClick.RemoveListener(Select);
        selectButton.onClick.AddListener(Deselect);
    }

    public void Deselect()
    {
        GameManager.Instance.PlayerOrder.First.Value.PickedResults[Data.ID - 1] = null;
        frame.color = Color.black;

        selectButton.onClick.RemoveListener(Deselect);
        selectButton.onClick.AddListener(Select);
    }

    public void Roll()
    {
        ResultFace = Data.Faces[UnityEngine.Random.Range(0, Data.Faces.Length)];
        Data.Result = ResultFace;

        faceImg.sprite = ResultFace.sprite;
        if (ResultFace.isGolden)
        {
            goldenMarkImg.gameObject.SetActive(true);
        }
        else
        {
            goldenMarkImg.gameObject.SetActive(false);
        }

        selectButton.onClick.RemoveListener(Deselect);
        selectButton.onClick.AddListener(Select);

        if (GameManager.Instance.PlayerOrder.First.Value.TurnCounter == 3)
        {
            Select();
            selectButton.onClick.RemoveAllListeners();
            selectButton.gameObject.SetActive(false);
        }
    }
}

[Serializable]
public class Face
{
    public string name;
    public Sprite sprite;
    public bool isGolden;
}