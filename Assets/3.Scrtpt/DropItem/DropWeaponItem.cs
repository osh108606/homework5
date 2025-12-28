using UnityEngine;

public class DropWeaponItem : DropItem
{
    public int grade;
    public ItemGrade itemGrade;
    public WeaponAbility weaponAbility;
    public override void Drop(string weaponName)
    {
        base.Drop(weaponName);
        weaponAbility = new WeaponAbility();
        grade = Random.Range(0, (int)ItemGrade.Count);
        itemGrade = (ItemGrade)grade;
        weaponAbility.grade = grade;
        weaponAbility.itemGrade = itemGrade;
        //고정타입
        int typeIdx = (int)Resources.Load<WeaponData>("WeaponData/" + weaponName).weaponType;
        WeaponTypeDamage weaponTypeDamage = (WeaponTypeDamage)typeIdx;
        WeaponSubElement subElement = (WeaponSubElement)typeIdx;//값범위도 타입따라 다를예정
        //랜덤타입
        int randomIdx = Random.Range(0, (int)WeaponSubElement.Count);
        WeaponSubElement subRandomElement = (WeaponSubElement)randomIdx;
        int telIdx = Random.Range(0,(int)WeaponTelent.Count);
        WeaponTelent weaponTelent = (WeaponTelent)telIdx;

        WeaponTypeDamageData weaponTypeDamageData = new WeaponTypeDamageData();
        weaponTypeDamageData.weaponTypeDamage = weaponTypeDamage;

        WeaponSubElementData weaponSubElementData = new WeaponSubElementData();
        weaponSubElementData.weaponSubElement = subElement;

        WeaponSubElementData weaponRandomSubElementData = new WeaponSubElementData();
        weaponRandomSubElementData.weaponSubElement = subRandomElement;
        if (grade == 0)//WeaponTypeDamage
        {
            weaponTypeDamageData.value = Random.Range(1f, 3f);
            weaponAbility.weaponTypeDamageData = weaponTypeDamageData;
        }
        else if (grade == 1)//WeaponTypeDamage & WeaponSubElement
        {
            weaponTypeDamageData.value = Random.Range(3f, 6f);
            weaponAbility.weaponTypeDamageData = weaponTypeDamageData;
            weaponSubElementData.value = Random.Range(3f, 6f);
            weaponAbility.weaponSubElementData = weaponSubElementData;
        }
        else if (grade == 2)//WeaponTypeDamage & WeaponSubElement & WeaponSubElement(random)
        {
            weaponTypeDamageData.value = Random.Range(6f, 9f);
            weaponAbility.weaponTypeDamageData = weaponTypeDamageData;
            weaponSubElementData.value = Random.Range(6f, 9f);
            weaponAbility.weaponSubElementData = weaponSubElementData;
            weaponRandomSubElementData.value = Random.Range(6f, 9f);
            weaponAbility.weaponSubElementDatas.Add(weaponRandomSubElementData);
        }
        else if(grade == 3)//WeaponTypeDamage & WeaponSubElement & WeaponSubElement(random) & WeaponTelent(random)
        {
            weaponTypeDamageData.value = Random.Range(9f, 12f);
            weaponAbility.weaponTypeDamageData = weaponTypeDamageData;
            weaponSubElementData.value = Random.Range(9f, 12f);
            weaponAbility.weaponSubElementData = weaponSubElementData;
            weaponRandomSubElementData.value = Random.Range(6f, 9f);
            weaponAbility.weaponSubElementDatas.Add(weaponRandomSubElementData);
            weaponAbility.weaponTelent.Add(weaponTelent);
        }
        else if (grade == 4)//WeaponTypeDamage & WeaponSubElement & WeaponSubElement(random) & WeaponTelent(random)
        {
            weaponTypeDamageData.value = Random.Range(12f, 15f);
            weaponAbility.weaponTypeDamageData = weaponTypeDamageData;
            weaponSubElementData.value = Random.Range(12f, 15f);
            weaponAbility.weaponSubElementData = weaponSubElementData;
            weaponRandomSubElementData.value = Random.Range(6f, 9f);
            weaponAbility.weaponSubElementDatas.Add(weaponRandomSubElementData);
            weaponAbility.weaponTelent.Add(weaponTelent);
        }

    }
}

