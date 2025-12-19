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
public enum SubElement
{
    Unfixed,//지정안됨 랜덤일경우
    CriticalDamage,
    CriticalChance,
    HeadShotDamage,
}
public enum AmmorTelent
{
    Telent1,
    Telent2,
    Telent3,
}
public class Ammor : MonoBehaviour
{ 
    public string key; //장비구분
    public string uid; // "이" 장비구분
    public int idx;
    public int ammorPoint;
    public AmmorData ammorData; 
    public AmmorEquipSlot ammorEquipSlot;
    public MainElement mainElement;
    public SubElement subElement1;
    public SubElement subElement2;
    public AmmorTelent ammorTelent;


    public void Awake()
    {
        ammorData = Resources.Load<AmmorData>("EquipmentData/" + key);
        mainElement = ammorData.mainElement;
    }
}
