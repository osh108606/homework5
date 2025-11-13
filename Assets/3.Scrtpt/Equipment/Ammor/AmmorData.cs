using UnityEngine;


[CreateAssetMenu(fileName = "AmmorData", menuName = "Scriptable Objects/AmmorData")]
public class AmmorData : ScriptableObject
{
    public string key;
    public int armorPoint;
    public string ammorName;
    public Sprite sprite;
    public AmmorEquipSlot ammorEquipSlot;
    public MainElement mainElement;
}
