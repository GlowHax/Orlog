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
    [SerializeField] private Button backButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private TMP_Text titleText;

    [SerializeField] private List<Godfavor> selectedGodFavors = new List<Godfavor>();
    private Dictionary<string, Godfavor> godFavors;

    private void Awake()
    {
        LoadGodFavors();
    }

    private void Start()
    {
        backButton.onClick.AddListener(() => BackToMainMenu());
        nextButton.onClick.AddListener(() => Next());
        nextButton.interactable = false;
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
            GameManager.Instance.player1.Godfavors = selectedGodFavors.ToArray();
            selectedGodFavors.Clear();
            titleText.text = "Player 2";
            RefreshScrollContent();
        }
        else if(selectedGodFavors.Count == 6)
        {
            GameManager.Instance.player2.Godfavors = selectedGodFavors.ToArray();
            GameManager.Instance.ChangeState(GameState.Running);
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
            GameObject currentPrefab = Instantiate(godFavorSelectPrefab, scrollContent.transform);
            currentPrefab.name = entry.Key;
            Button button = currentPrefab.GetComponent<Button>();
            button.onClick.AddListener(() => AddSelectedGodFavor(button, entry.Value));
            TMP_Text text = currentPrefab.GetComponentInChildren<TMP_Text>();
            text.text = entry.Key;
        }
    }

    private void AddSelectedGodFavor(Button button, Godfavor godFavor)
    {
        
        if(selectedGodFavors.Count == 2)
        {
            selectedGodFavors.Add(godFavor);
            button.interactable = false;
            nextButton.interactable = true;

            List<GameObject> childObjects = new List<GameObject>();
            for (int i = 0; i < scrollContent.transform.childCount; i++)
            {
                childObjects.Add(scrollContent.transform.GetChild(i).gameObject);
                foreach (Godfavor favor in selectedGodFavors)
                {
                    if(scrollContent.transform.GetChild(i).name == favor.Name)
                    {
                        childObjects.Remove(scrollContent.transform.GetChild(i).gameObject);
                    }
                }
            }

            foreach (GameObject obj in childObjects)
            {
                Destroy(obj);
            }
        }
        else
        {
            selectedGodFavors.Add(godFavor);
            button.interactable = false;
        }
        
    }
}
