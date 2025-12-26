using System;
using UnityEngine;


[CreateAssetMenu(fileName = "AmmorData", menuName = "Scriptable Objects/AmmorData")]
public class AmmorData : ScriptableObject
{
    public string key;
    public string ammorName;
    public Sprite sprite;
    public AmmorEquipSlot ammorEquipSlot;
    public AmmorMainElement ammorMainElement1; //아머 주능력치1(필수)
    public AmmorMainElement ammorMainElement2; //아머 주능력치2(조건)
    public AmmorMainElement ammorMainElement3; //아머 주능력치3(조건)
    public AmmorSubElement ammorSubElement1; //아머 보조유동능력치1(필수)
    public AmmorSubElement ammorSubElement2; //아머 보조유동능력치2(조건)
    public AmmorSubElement ammorSubElement3; ////아머 보조유동능력치3(조건)
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
