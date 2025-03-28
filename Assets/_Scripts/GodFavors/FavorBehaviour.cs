using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FavorBehaviour : MonoBehaviour
{
    public string Name;
    public FavorOption[] Options = new FavorOption[3];
    public int Priority;
    public ResolveEffect EffectResolutionTime;
    [Multiline(8)] public string Description;

    public virtual void ResolveEffect(Player owner, FavorOption selectedOption)
    {
        GameManager.Instance.ResolveNextGodFavor();
    }
}