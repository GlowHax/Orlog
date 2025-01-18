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
    [SerializeField] private TMP_InputField playerNameInputField;
    [SerializeField] private Button backButton;
    [SerializeField] private Button nextButton;

    [SerializeField] private List<GodFavor> selectedGodFavors = new List<GodFavor>();

    private int activeChoosingPlayer = 1;
    private Dictionary<string, GodFavor> godFavors;

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
        if(activeChoosingPlayer == 1)
        {
            foreach(GodFavor favor in selectedGodFavors)
            {
                favor.owner = GameManager.Instance.Player1;
            }
            GameManager.Instance.Player1.Godfavors = selectedGodFavors.ToArray();
            selectedGodFavors.Clear();
            activeChoosingPlayer = 2;
            playerNameInputField.text = "Player 2";
            RefreshScrollContent();
            nextButton.interactable = false;
        }
        else if(activeChoosingPlayer == 2)
        {
            foreach (GodFavor favor in selectedGodFavors)
            {
                favor.owner = GameManager.Instance.Player2;
            }
            GameManager.Instance.Player2.Godfavors = selectedGodFavors.ToArray();
            GameManager.Instance.ChangeState(GameState.Starting);
        }
    }

    public void SubmitPlayerName(string Name)
    {
        if(activeChoosingPlayer == 1)
        {
            GameManager.Instance.Player1.Name = Name;
        }
        else
        {
            if (Name == GameManager.Instance.Player1.Name)
            {
                GameManager.Instance.Player2.Name = Name + " (2)";
            }
            else
            {
                GameManager.Instance.Player2.Name = Name;
            }
        }
    }

    private void LoadGodFavors()
    {
        godFavors = Resources.LoadAll<GodFavor>("GodFavors").ToDictionary(r => r.Name, r => r);
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
        foreach (KeyValuePair<string, GodFavor> entry in godFavors) 
        {
            GameObject currentPrefab = Instantiate(godFavorSelectPrefab, scrollContent.transform);
            currentPrefab.name = entry.Key;
            Button button = currentPrefab.GetComponent<Button>();
            button.onClick.AddListener(() => AddSelectedGodFavor(button, entry.Value));
            TMP_Text text = currentPrefab.GetComponentInChildren<TMP_Text>();
            text.text = entry.Key;
        }
    }

    private void AddSelectedGodFavor(Button button, GodFavor godFavor)
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
                foreach (GodFavor favor in selectedGodFavors)
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
