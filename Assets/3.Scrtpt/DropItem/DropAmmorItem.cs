using UnityEngine;

public class DropAmmorItem : DropItem
{
    public int grade;
    public ItemGrade itemGrade;
    public AmmorAbility ammorAbility;
    public override void Drop(string ammorName)
    {
        base.Drop(ammorName);
        {
            ammorAbility = new AmmorAbility();
            grade = Random.Range(0, (int)ItemGrade.Count);
            itemGrade = (ItemGrade)grade;
            ammorAbility.grade = grade;
            ammorAbility.itemGrade = itemGrade;
            AmmorEquipSlot ammorEquipSlot = Resources.Load<AmmorData>("AmmorData/" + ammorName).ammorEquipSlot;
            AmmorMainElementData ammorMainElementData = AmmorManager.Instance.GetAmmorElementData(Resources.Load<AmmorData>("AmmorData/" + ammorName).ammorBrand);

            // 고정타입
            // 브랜드 혹은 아머타입(슬롯)별 고정 능력치

            ammorAbility.ammorMainsElementData = new AmmorMainsElementData();
            ammorAbility.ammorMainsElementData.ammorMainElement = Resources.Load<AmmorData>("AmmorData/" + ammorName).ammorMainElements[0];
        }
    }
}
