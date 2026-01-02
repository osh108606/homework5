using System;
using UnityEngine;


[CreateAssetMenu(fileName = "AmmorData", menuName = "Scriptable Objects/AmmorData")]
public class AmmorData : ScriptableObject
{
    public string key;
    public int idx;
    public string ammorName;
    public float ammorPoint;

    public Sprite sprite;
    public AmmorEquipSlot ammorEquipSlot;
    public AmmorBrand ammorBrand;
    public AmmorMainElement[] ammorMainElements; //아머 주능력치
    public AmmorAbility ammorAbility;
    public AmmorTelent ammorTelent;
}

