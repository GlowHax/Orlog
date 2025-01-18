using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FavorPhaseView : View
{
    [SerializeField] private GameObject godFavorSection;
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text tokenCounterText;
    [SerializeField] private Button skipButton;
    [SerializeField] private Button[] favorOptionButtons = new Button[9];
    [SerializeField] private TMP_Text[] favorTitleTexts = new TMP_Text[3];
    

    private void Start()
    {
        InitFavorSelection(GameManager.Instance.Player1);
        skipButton.onClick.AddListener(() => { 
            InitFavorSelection(GameManager.Instance.Player2);
            skipButton.onClick.RemoveAllListeners();
            skipButton.onClick.AddListener(() => 
            {
                GameManager.Instance.ChangeState(GameState.ResolutionPhase);
                skipButton.onClick.RemoveAllListeners();
            });
        });
    }

    private void InitFavorSelection(Player player)
    {
        godFavorSection.SetActive(true);

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
                    {
                        player.selectedGodfavor = player.Godfavors[i];
                        player.selectedGodfavor.selectedOption = option;
                        GameManager.Instance.ChangeState(GameState.ResolutionPhase);
                    });
                    favorOptionButtons[j + difference].interactable = true;
                }
            }
            difference += 3;
        }
    }
}