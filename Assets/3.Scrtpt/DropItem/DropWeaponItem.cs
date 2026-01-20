using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
        WeaponType weaponType = Resources.Load<WeaponData>("WeaponData/" + weaponName).weaponType;
        WeaponTypeElementData elementData = WeaponManager.Instance.GetWeaponTypeElementData(weaponType);
        
        //고정타입
        //무기 타입별 추가데미지
        weaponAbility.weaponTypeDamageData = new WeaponTypeDamageData();
        weaponAbility.weaponTypeDamageData.weaponType = weaponType;
        weaponAbility.weaponTypeDamageData.value = Random.Range(elementData.addWeaponDamageDataValues[grade].x, elementData.addWeaponDamageDataValues[grade].y);

        //무기 타입 별 능력치
        if(weaponAbility.grade >= 1 )
        {
            WeaponSubElement subElement = elementData.fixWeaponSubElement;
            weaponAbility.weaponFixSubElementData = new WeaponSubElementData();
            weaponAbility.weaponFixSubElementData.weaponSubElement = subElement;
            weaponAbility.weaponFixSubElementData.value = Random.Range(elementData.fixWeaponSubElementValues[grade].x, elementData.fixWeaponSubElementValues[grade].y);
        }
        else
        {
            weaponAbility.weaponFixSubElementData.weaponSubElement = WeaponSubElement.Null;
            weaponAbility.weaponFixSubElementData.value = 0;
        }

            //보조능력치 풀
            List<WeaponSubElement> elements = new List<WeaponSubElement>();
        for(int i=0; i<(int)WeaponSubElement.Count; i++)
        {
            elements.Add((WeaponSubElement)i);
        }
        if(elementData.fixWeaponSubElement != WeaponSubElement.Null)
            elements.Remove(elementData.fixWeaponSubElement);

        //랜덤 타입별 능력치 설정
        for (int i = 0; i < elementData.randomElementCounts[grade];i++)
        {
            int randomIdx = Random.Range(0, elements.Count);
            WeaponSubElement subRandomElement = elements[randomIdx];

            WeaponSubElementData weaponRandomSubElementData = new WeaponSubElementData();
            weaponRandomSubElementData.weaponSubElement = subRandomElement;
            RandomWeaponSubElementData randomElementData = WeaponManager.Instance.GetRandomElementData(weaponRandomSubElementData.weaponSubElement);
            weaponRandomSubElementData.value = Random.Range(randomElementData.weaponSubElementValues[grade].x, randomElementData.weaponSubElementValues[grade].y);
            weaponAbility.weaponRandomSubElementDates.Add(weaponRandomSubElementData);

            if (weaponRandomSubElementData.weaponSubElement != WeaponSubElement.Null)
                elements.Remove(subRandomElement);
        }
        int telIdx = Random.Range(0, (int)WeaponTelent.Count);
        WeaponTelent weaponTelent = (WeaponTelent)telIdx;
    }
}

