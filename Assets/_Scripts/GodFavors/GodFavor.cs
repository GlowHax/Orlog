using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GodFavor : MonoBehaviour
{
    public string Name;
    public FavorOption[] Options = new FavorOption[3];
    public int Priority;
    public bool IsResolvedInstant;
    public View extraView;
    [Multiline(8)] public string Description;

    public FavorOption selectedOption;
    [HideInInspector] public Player owner;

    public static event Action OnGodFavorEffectResolved;

    public virtual void ResolveEffect()
    {
        OnGodFavorEffectResolved?.Invoke();
    }
}

[Serializable]
public class FavorOption
{
    public int Cost;
    public int Value;
    public string ButtonText;
}