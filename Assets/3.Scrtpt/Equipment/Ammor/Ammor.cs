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
public enum MainElement
{
    Attack,
    Defense,
    Skill
}
public class Ammor : MonoBehaviour
{ 
    public string key;
    public int idx;
    public AmmorData ammorData; 
    public AmmorEquipSlot ammorEquipSlot;
    public MainElement mainElement;



    public void Awake()
    {
        ammorData = Resources.Load<AmmorData>("EquipmentData/" + key);
        mainElement = ammorData.mainElement;
    }
}
