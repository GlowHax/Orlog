using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        choosingPlayer = GameManager.Instance.Player1;
        InitFavorSelection(choosingPlayer);
        nextButton.onClick.AddListener(() => { 
            InitFavorSelection(GameManager.Instance.Player2);
            nextButton.onClick.RemoveAllListeners();
            nextButton.onClick.AddListener(() => 
            {
                GameManager.Instance.ChangeState(GameState.FavorResolution);
                nextButton.onClick.RemoveAllListeners();
            });
        });
    }

    private void InitFavorSelection(Player player)
    {
        playerNameText.text = player.Name;
        tokenCounterText.text = player.FavorTokens.ToString();

        int difference = 0;
        for (int i = 0; i < player.Godfavors.Length; i++)
        {
            favorTitleTexts[i].text = player.Godfavors[i].Name;
            for(int j = 0; j < player.Godfavors[i].Options.Length; j++)
            {
                FavorOption option = player.Godfavors[i].Options[j];
                favorOptionButtons[j+difference].GetComponentInChildren<TMP_Text>().text = option.ButtonText;

                if(player.FavorTokens >= option.Cost || !player.Godfavors[i].IsResolvedInstant)
                {
                    favorOptionButtons[j + difference].onClick.AddListener(() => 
                    SelectOption(player, player.Godfavors[i], option));
                    favorOptionButtons[j + difference].interactable = true;
                }
            }
            difference += 3;
        }
    }

    public void SelectOption(Player player, GodFavor favor, FavorOption option)
    {
        player.selectedGodfavor = favor;
        player.selectedGodfavor.selectedOption = option;
        if (player.selectedGodfavor.IsResolvedInstant)
        {
            GameManager.Instance.AddGodFavorToResolutionList(player.selectedGodfavor);
        }
    }
}