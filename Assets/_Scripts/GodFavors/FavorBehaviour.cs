using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FavorBehaviour : MonoBehaviour
{
    public string Name;
    public FavorOption[] Options = new FavorOption[3];
    public int Priority;
    public ResolveEffect EffectResolutionTime;
    [Multiline(8)] public string Description;

    public virtual void ResolveEffect(GodFavor godFavor)
    {
    }

    public void ShowNotEnoughFavorTokenView(GodFavor godFavor)
    {
        UIManager.Instance.ShowView("NotEnoughFavorTokenView");
        NotEnoughFavorTokenView view = UIManager.Instance.CurrentView as NotEnoughFavorTokenView;
        if (view != null)
        {
            view.Init(godFavor);
        }
    }
}