using UnityEngine;
using UnityEngine.Serialization;

public class ArmorManager : MonoBehaviour
{
    public static ArmorManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    public ArmorMainElementData[] armorMainElementDates;
    public RandomArmorSubElementData[] randomArmorSubElementDates;
    public ArmorMainElementData GetArmorElementData(ArmorBrand armorBrand)
    {
        for (int i = 0; i < armorMainElementDates.Length; i++)
        {
            if (armorMainElementDates[i].armorBrand == armorBrand)
            {
                return armorMainElementDates[i];
            }
        }
        return null;
    }

    public RandomArmorSubElementData GetArmorRandomElementData(ArmorSubElement armorSubElement)
    {
        for (int i = 0; i < randomArmorSubElementDates.Length; i++)
        {
            if (randomArmorSubElementDates[i].armorSubElement == armorSubElement)
            {
                return randomArmorSubElementDates[i];
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