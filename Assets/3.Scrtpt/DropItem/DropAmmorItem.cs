using UnityEngine;

public class DropAmmorItem : DropItem
{
    public int grade;
    public ItemGrade itemGrade;
    public override void Drop(string key)
    {
        base.Drop(key);
        grade = Random.Range(0, 6);
        switch (grade)
        {
            case 0:
                itemGrade = ItemGrade.Common;
                break;
                
            case 1:
                itemGrade = ItemGrade.Normal;
                break;
                
            case 2:
                itemGrade = ItemGrade.Rare;
                break;
                
            case 3:
                itemGrade = ItemGrade.Epic;
                break;
            case 4:
                itemGrade = ItemGrade.Legendary;
                break;
            case 5:
                itemGrade = ItemGrade.Exotic;
                break;
            case 6:
                itemGrade = ItemGrade.Set;
                break;
            default:
                break;

        }
    }
}
