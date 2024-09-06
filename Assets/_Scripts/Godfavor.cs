using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New GodFavor", menuName = "GodFavor")]
public class Godfavor : ScriptableObject
{
    public string Name;
    public FavorOption[] Options = new FavorOption[3];
    public int Priority;
    [Multiline(8)] public string Description;
}

[Serializable]
public class FavorOption
{
    public int Cost;
    public int Value;
    public string ButtonText;
}
