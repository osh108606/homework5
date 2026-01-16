using UnityEngine;

public class DropArmorItem : DropItem
{
    public int grade;
    public ItemGrade itemGrade;
    public ArmorAbility armorAbility;
    public override void Drop(string armorName)
    {
        base.Drop(armorName);
        {
            armorAbility = new ArmorAbility();
            grade = Random.Range(0, (int)ItemGrade.Count);
            itemGrade = (ItemGrade)grade;
            armorAbility.grade = grade;
            armorAbility.itemGrade = itemGrade;
            ArmorEquipSlot armorEquipSlot = Resources.Load<ArmorData>("ArmorData/" + armorName).armorEquipSlot;
            ArmorMainElementData armorMainElementData = ArmorManager.Instance.GetArmorElementData(Resources.Load<ArmorData>("ArmorData/" + armorName).armorBrand);

            // 고정타입
            // 브랜드 혹은 아머타입(슬롯)별 고정 능력치

            armorAbility.armorMainsElementData = new ArmorMainsElementData();
            armorAbility.armorMainsElementData.armorMainElement = Resources.Load<ArmorData>("ArmorData/" + armorName).armorMainElements[0];
        }
    }
}
