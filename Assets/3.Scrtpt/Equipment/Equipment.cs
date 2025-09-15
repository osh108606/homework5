using UnityEngine;

public class Equipment : MonoBehaviour
{
    public int idx;
    public EquipmentData equipmentData;
    public string key;

    public void Awake()
    {
        equipmentData = Resources.Load<EquipmentData>("EquipmentData/" + key);
    }
}
