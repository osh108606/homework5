using System;
using UnityEngine;


[CreateAssetMenu(fileName = "ArmorData", menuName = "Scriptable Objects/ArmorData")]
public class ArmorData : ScriptableObject
{
    public string key;
    public int idx;
    public string armorName;
    public float armorPoint;

    public Sprite sprite;
    public ArmorEquipSlot armorEquipSlot;
    public ArmorBrand armorBrand;
    public ArmorMainElement[] armorMainElements; //아머 주능력치
    public ArmorAbility armorAbility;
    public ArmorTelent armorTelent;
}

