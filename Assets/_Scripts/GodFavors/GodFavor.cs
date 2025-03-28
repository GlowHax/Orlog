using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodFavor
{
    public FavorBehaviour Behaviour;

    public FavorOption selectedOption;
    /*[HideInInspector]*/ public Player owner;

    public GodFavor(FavorBehaviour behaviour)
    {
        this.Behaviour = behaviour;
    }

    public void ResolveEffect()
    {
        Behaviour.ResolveEffect(owner, selectedOption);
    }
}
