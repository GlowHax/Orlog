using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class OdinsSacrificeView : View
{
    public TMP_Text TitleText;
    public Slider _Slider;
    public TMP_Text HPCounterText;
    public TMP_Text FavorTokenCounterText;
    public Button NextButton;

    private void Start()
    {
        NextButton.onClick.AddListener(() => GameManager.Instance.ResolveNextGodFavor());
    }

    public void UpdateCounters()
    {
        HPCounterText.text = _Slider.value.ToString();
        FavorTokenCounterText.text = _Slider.value.ToString();
    }
}