using UnityEngine;

public class DropWeaponItem : DropItem
{
    int grade;
    public override void Drop(string key)
    {
        base.Drop(key);
        grade = Random.Range(0, 6);

    }
}

