using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ResolveEffect
{
    BeforeResolutionPhase,
    AfterResolutionPhase
}

[Serializable]
[CreateAssetMenu(fileName = "New GodFavorData", menuName = "GodFavorData")]
public class GodFavorData : ScriptableObject
{
    public string Name;
    public FavorOption[] Options = new FavorOption[3];
    public int Priority;
    public ResolveEffect EffectResolutionTime;
    [Multiline(8)] public string Description;
    public View view;
}

[Serializable]
public class FavorOption
{
    public int Cost;
    public int Value;
    public string ButtonText;
}