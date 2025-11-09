using UnityEngine;
public enum AmmorEquipSlot
{
    num1, num2, num3, num4, num5, num6
}
public class Ammor : MonoBehaviour
{
    public AmmorEquipSlot ammorEquipSlot;
    public int idx;
    public AmmorData ammorData;
    public string key;

    public void Awake()
    {
        ammorData = Resources.Load<AmmorData>("EquipmentData/" + key);
    }
}
