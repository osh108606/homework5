using UnityEngine;


public enum ItemGrade
{
    Common,
    Normal,
    Rare,
    Epic,
    Legendary,
    Exotic,
    Set
}
public enum WeaponTypeDamage
{
    HG,
    AR,
    SMG,
    MG,
    RF,
    SR,
    SG,
}

public enum WeaponSubElement
{
    Null,
    Unfixed,//ÁöÁ¤¾ÈµÊ ·£´ýÀÏ°æ¿ì
    CriticalDamage,
    CriticalChance,
    HeadShotDamage,
    Accuracy,
    Recoil,
    APDamage,
    HPDamage,
    UnCoverDamage
}

public enum WeaponTelent
{
    Null, 
    Telent1,
    Telent2,
    Telent3,
}

public enum AmmorMainElement
{
    Null,
    Attack,
    Defense,
    Skill
}
public enum AmmorSubElement
{
    Null,
    Unfixed,//ÁöÁ¤¾ÈµÊ ·£´ýÀÏ°æ¿ì
    CriticalDamage,
    CriticalChance,
    HeadShotDamage,
}
public enum AmmorTelent
{
    Null,
    Telent1,
    Telent2,
    Telent3,
}
public class ItemElement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
