using UnityEngine;

public class AmmorManager : MonoBehaviour
{
    public static AmmorManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    public AmmorMainElementData[] ammorMainElementDatas;
    public RandomAmmorSubElementData[] randomAmmorSubElementDatas;
    public AmmorMainElementData GetAmmorElementData(AmmorBrand ammorBrand)
    {
        for (int i = 0; i < ammorMainElementDatas.Length; i++)
        {
            if (ammorMainElementDatas[i].ammorBrand == ammorBrand)
            {
                return ammorMainElementDatas[i];
            }
        }
        return null;
    }

    public RandomAmmorSubElementData GetAmmorRandomElementData(AmmorSubElement ammorSubElement)
    {
        for (int i = 0; i < randomAmmorSubElementDatas.Length; i++)
        {
            if (randomAmmorSubElementDatas[i].ammorSubElement == ammorSubElement)
            {
                return randomAmmorSubElementDatas[i];
            }

        }
        return null;
    }
}
[System.Serializable]
public class AmmorMainElementData
{
    public AmmorBrand ammorBrand;
    public AmmorEquipSlot ammorEquipSlot;   
    public AmmorMainElement ammorMainElement;
    public Vector2[] addAmmorElementDataValues;

    public int[] randomElementCounts;
}
[System.Serializable]
public class RandomAmmorSubElementData
{
    public AmmorSubElement ammorSubElement;
    public Vector2[] ammorSubElementValues;
}