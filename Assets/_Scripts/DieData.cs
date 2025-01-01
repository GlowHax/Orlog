using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New DieData", menuName = "DieData")]
public class DieData : ScriptableObject
{
    public int ID;
    public Face Result;
    public Face[] Faces = new Face[6];
}
