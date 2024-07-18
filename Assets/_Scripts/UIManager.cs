using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject canvas;

    public void ShowMainMenu()
    {
        ClearAllActivePanels();
        Instantiate(mainMenuPanel, canvas.transform);
    }

    public void ClearAllActivePanels()
    {
        while(canvas.transform.childCount > 0)
        {
            Destroy(canvas.transform.GetChild(0).gameObject);
        }
    }
}
