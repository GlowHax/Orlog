using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionPhaseView : MonoBehaviour
{
    [SerializeField] private Button applyButton;

    [SerializeField] private TMP_Text player1NameText;
    [SerializeField] private HealthBar player1HealthBar;
    [SerializeField] private TMP_Text player1TokenCounterText;

    [SerializeField] private TMP_Text player2NameText;
    [SerializeField] private HealthBar player2HealthBar;
    [SerializeField] private TMP_Text player2TokenCounterText;
}
