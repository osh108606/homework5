using UnityEngine;
public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    public WeaponTypeElementData[] weaponTypeElementDatas;
    public RandomWeaponSubElementData[] randomWeaponSubElementDatas;
    public WeaponTypeElementData GetWeaponTypeElementData(WeaponType weaponType)
    {
        for (int i = 0; i < weaponTypeElementDatas.Length; i++)
        {
            if (weaponTypeElementDatas[i].weaponType == weaponType)
            {
                return weaponTypeElementDatas[i];
            }
        }
        return null;
    }

    public RandomWeaponSubElementData GetRandomElementData(WeaponSubElement weaponSubElement)
    {
        for(int i = 0; i< randomWeaponSubElementDatas.Length;i++)
        {
            if (randomWeaponSubElementDatas[i].weaponSubElement == weaponSubElement)
            {
                return randomWeaponSubElementDatas[i];
            }

        }
        return null;
    }
} 


[System.Serializable]
public class WeaponTypeElementData
{
    public WeaponType weaponType;
    public Vector2[] addWeaponDamageDataValues;
    public WeaponSubElement fixWeaponSubElement;
    public Vector2[] fixWeaponSubElementValues;

    public int[] randomElementCounts;
}

[System.Serializable]
public class RandomWeaponSubElementData
{
    public WeaponSubElement weaponSubElement;
    public Vector2[] weaponSubElementValues;
}
