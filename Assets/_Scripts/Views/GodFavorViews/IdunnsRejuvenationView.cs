using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IdunnsRejuvenationView : View
{
    public TMP_Text TitleText;
    public TMP_Text EffectText;
    public Button NextButton;

    private void Start()
    {
        NextButton.onClick.AddListener(() => GameManager.Instance.ResolveNextGodFavor());
    }
}
