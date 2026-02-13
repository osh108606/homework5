using UnityEngine;
public enum ArmorEquipSlot
{
    Slot1, 
    Slot2, 
    Slot3, 
    Slot4, 
    Slot5, 
    Slot6
}


public class Armor : MonoBehaviour
{ 
    public string key; //장비구분
    public string uid; // "이" 장비구분
    public int idx;
    public int armorPoint;
    public ArmorData armorData; 
    public ArmorEquipSlot armorEquipSlot;
    public ArmorMainElement armorMainElement;
    public ArmorSubElement subElement1;
    public ArmorSubElement subElement2;
    public ArmorTalent armorTalent;


    public void Awake()
    {
        armorData = Resources.Load<ArmorData>("EquipmentData/" + key);
        //armorMainElement = armorData.armorMainElement1;
    }
}
