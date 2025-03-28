using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IdunnsRejuvenationView : View
{
    public TMP_Text titleText;
    public TMP_Text effectText;
    public Button nextButton;

    private void Start()
    {
        nextButton.onClick.AddListener(() => GameManager.Instance.ResolveNextGodFavor());
    }
}
