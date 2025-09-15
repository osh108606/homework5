using UnityEngine;


[CreateAssetMenu(fileName = "EquipmentData", menuName = "Scriptable Objects/EquipmentData")]
public class EquipmentData : ScriptableObject
{
    public string key;
    public int armorPoint;
    public string equipmentName;
    public Sprite sprite;
}
