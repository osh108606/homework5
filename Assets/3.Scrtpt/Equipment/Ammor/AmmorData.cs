using System;
using UnityEngine;


[CreateAssetMenu(fileName = "AmmorData", menuName = "Scriptable Objects/AmmorData")]
public class AmmorData : ScriptableObject
{
    public string key;
    public string ammorName;
    public Sprite sprite;
    public AmmorEquipSlot ammorEquipSlot;
    public MainElement mainElement;
    public SubElement subElement1;
    public SubElement subElement2;
    public AmmorTelent ammorTelent;

    [Serializable]
    public struct StatRange
    {
        public string statKey;
        public float min;
        public float max;
    }

    [Serializable]
    public struct TalentRange
    {
        public string talentKey;
        public float min;
        public float max;
    }
}
