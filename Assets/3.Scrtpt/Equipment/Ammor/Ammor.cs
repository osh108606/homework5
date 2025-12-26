using UnityEngine;
public enum AmmorEquipSlot
{
    Slot1, 
    Slot2, 
    Slot3, 
    Slot4, 
    Slot5, 
    Slot6
}


public class Ammor : MonoBehaviour
{ 
    public string key; //장비구분
    public string uid; // "이" 장비구분
    public int idx;
    public int ammorPoint;
    public AmmorData ammorData; 
    public AmmorEquipSlot ammorEquipSlot;
    public AmmorMainElement ammorMainElement;
    public AmmorSubElement subElement1;
    public AmmorSubElement subElement2;
    public AmmorTelent ammorTelent;


    public void Awake()
    {
        ammorData = Resources.Load<AmmorData>("EquipmentData/" + key);
        ammorMainElement = ammorData.ammorMainElement1;
    }
}
