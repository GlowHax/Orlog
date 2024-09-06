using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FavorSelectionMenu : View
{
    [SerializeField] private GameObject scrollContent;
    [SerializeField] private GameObject godFavorSelectPrefab;
    [SerializeField] private Button BackButton;
    [SerializeField] private Button NextButton;

    [SerializeField] private List<Godfavor> selectedGodFavors = new List<Godfavor>();
    private Dictionary<string, Godfavor> godFavors;

    private void Awake()
    {
        LoadGodFavors();
    }

    private void Start()
    {
        BackButton.onClick.AddListener(() => BackToMainMenu());
        NextButton.onClick.AddListener(() => Next());
        NextButton.interactable = false;
        RefreshScrollContent();
    }

    private void BackToMainMenu()
    {
        GameManager.Instance.ChangeState(GameState.MainMenu);
    }

    private void Next()
    {
        if(selectedGodFavors.Count == 3)
        {

        }
    }

    private void LoadGodFavors()
    {
        godFavors = Resources.LoadAll<Godfavor>("GodFavors").ToDictionary(r => r.Name, r => r);
    }

    private void ClearScrollContent()
    {
        List<GameObject> childObjects = new List<GameObject>();
        for (int i = 0; i < scrollContent.transform.childCount; i++)
        {
            childObjects.Add(scrollContent.transform.GetChild(i).gameObject);
        }

        foreach (GameObject obj in childObjects)
        {
            Destroy(obj);
        }
    }

    private void RefreshScrollContent()
    {
        ClearScrollContent();
        foreach (KeyValuePair<string, Godfavor> entry in godFavors) 
        {
            godFavorSelectPrefab = Instantiate(godFavorSelectPrefab, scrollContent.transform);
            Button button = godFavorSelectPrefab.GetComponent<Button>();
            button.onClick.AddListener(() => AddSelectedGodFavor(button, entry.Value));
            TMP_Text text = godFavorSelectPrefab.GetComponentInChildren<TMP_Text>();
            text.text = entry.Key;
        }
    }

    private void AddSelectedGodFavor(Button button, Godfavor godFavor)
    {
        if(selectedGodFavors.Count < 2)
        {
            selectedGodFavors.Add(godFavor);
            button.interactable = false;
        }
        else
        {
            selectedGodFavors.Add(godFavor);
            button.interactable = false;
            NextButton.interactable = true;
        }
    }
}
