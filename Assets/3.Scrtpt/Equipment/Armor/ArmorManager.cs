using UnityEngine;

public class ArmorManager : MonoBehaviour
{
    public static ArmorManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    public ArmorMainElementData[] armorMainElementDatas;
    public RandomArmorSubElementData[] randomArmorSubElementDatas;
    public ArmorMainElementData GetArmorElementData(ArmorBrand armorBrand)
    {
        for (int i = 0; i < armorMainElementDatas.Length; i++)
        {
            if (armorMainElementDatas[i].armorBrand == armorBrand)
            {
                return armorMainElementDatas[i];
            }
        }
        return null;
    }

    public RandomArmorSubElementData GetArmorRandomElementData(ArmorSubElement armorSubElement)
    {
        for (int i = 0; i < randomArmorSubElementDatas.Length; i++)
        {
            if (randomArmorSubElementDatas[i].armorSubElement == armorSubElement)
            {
                return randomArmorSubElementDatas[i];
            }

        }
        return null;
    }
}
[System.Serializable]
public class ArmorMainElementData
{
    public ArmorBrand armorBrand;
    public ArmorEquipSlot armorEquipSlot;   
    public ArmorMainElement armorMainElement;
    public Vector2[] addArmorElementDataValues;

    public int[] randomElementCounts;
}
[System.Serializable]
public class RandomArmorSubElementData
{
    public ArmorSubElement armorSubElement;
    public Vector2[] armorSubElementValues;
}