using UnityEngine;


public class WeaponManager : MonoSingleton<WeaponManager>
{
    public WeaponTypeElementData[] weaponTypeElementDates;
    public RandomWeaponSubElementData[] randomWeaponSubElementDates;
    public WeaponTypeElementData GetWeaponTypeElementData(WeaponType weaponType)
    {
        for (int i = 0; i < weaponTypeElementDates.Length; i++)
        {
            if (weaponTypeElementDates[i].weaponType == weaponType)
            {
                return weaponTypeElementDates[i];
            }
        }
        return null;
    }

    public RandomWeaponSubElementData GetRandomElementData(WeaponSubElement weaponSubElement)
    {
        for(int i = 0; i< randomWeaponSubElementDates.Length;i++)
        {
            if (randomWeaponSubElementDates[i].weaponSubElement == weaponSubElement)
            {
                return randomWeaponSubElementDates[i];
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
