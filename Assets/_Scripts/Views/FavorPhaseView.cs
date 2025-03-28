using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FavorPhaseView : View
{
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text tokenCounterText;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button[] favorOptionButtons = new Button[9];
    [SerializeField] private TMP_Text[] favorTitleTexts = new TMP_Text[3];

    public Player choosingPlayer;

    private void Start()
    {
        InitFavorSelection();
        choosingPlayer = GameManager.Instance.BeginningPlayer;
    }

    private void InitFavorSelection()
    {
        GameManager.Instance.SwitchActivePlayer();
        choosingPlayer = GameManager.Instance.PlayerOrder.First.Value;
        playerNameText.text = choosingPlayer.Name;
        tokenCounterText.text = choosingPlayer.FavorTokens.ToString();

        int difference = 0;
        for (int i = 0; i < choosingPlayer.Godfavors.Length; i++)
        {
            favorTitleTexts[i].text = choosingPlayer.Godfavors[i].Behaviour.Name;
            for(int j = 0; j < choosingPlayer.Godfavors[i].Behaviour.Options.Length; j++)
            {
                GodFavor favor = choosingPlayer.Godfavors[i];
                FavorOption option = favor.Behaviour.Options[j];
                favorOptionButtons[j+difference].GetComponentInChildren<TMP_Text>().text = option.ButtonText;

                if(choosingPlayer.FavorTokens >= option.Cost || 
                    choosingPlayer.Godfavors[i].Behaviour.EffectResolutionTime == ResolveEffect.AfterResolutionPhase)
                {
                    favorOptionButtons[j + difference].onClick.RemoveAllListeners();
                    favorOptionButtons[j + difference].onClick.AddListener(() =>
                    {
                        SelectOption(favor, option);
                        Next();
                    });
                    favorOptionButtons[j + difference].interactable = true;
                }
                else
                {
                    favorOptionButtons[j + difference].interactable = false;
                }
            }
            difference += 3;
        }
    }

    public void SelectOption(GodFavor favor, FavorOption option)
    {
        favor.selectedOption = option;
        choosingPlayer.selectedGodfavor = favor;
        GameManager.Instance.AddGodFavorToResolutionList(favor);
    }

    public void Next()
    {
        if(choosingPlayer == GameManager.Instance.BeginningPlayer)
        {
            
            InitFavorSelection();
            nextButton.onClick.RemoveAllListeners();
            nextButton.onClick.AddListener(() =>
            {
                GameManager.Instance.ResolveNextGodFavor();
                nextButton.onClick.RemoveAllListeners();
            });
        }
        else
        {
            GameManager.Instance.ResolveNextGodFavor();
        }
    }
}