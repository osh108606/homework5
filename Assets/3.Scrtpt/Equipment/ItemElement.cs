using UnityEngine;


public enum ItemGrade
{
    Common,//회색
    Normal,//연초록
    Rare,//파랑
    Epic,//보라
    Legendary,//노랑 
    Count,
    Named,//진노랑 Legendary와 같은 등급
    Exotic,//빨강 Legendary와 같은 등급
    Set,//진초록 Legendary와 같은 등급
    Unfixed,//지정안됨 랜덤일경우
}

public enum WeaponSubElement
{
    CriticalChance, //SMG
    CriticalDamage, //AR

    HealthPointDamage, //SG 
    ArmorPointDamage, //RF

    PrecisionDamage, //SR    
    ArmorPlateDamage,//MG
    WeakPointDamage, // HG

    UnCoverDamage,//어쩌면 또다른 무기군

    Count,
    Accuracy, // random
    Recoil,
    
    Null,
    Unfixed,//지정안됨 랜덤일경우
}

public enum WeaponTelent
{
    Telent1,
    Telent2,
    Telent3,
    Count,
    Null,
    Unfixed,//지정안됨 랜덤일경우
}

public enum ArmorBrand
{
    Brand1,
    Brand2,
    Brand3,
    Brand4,
    Brand5,
    Brand6,
    Brand7,
    Brand8,
    Count
}

public enum ArmorMainElement
{
    
    Attack,
    Defense,
    Skill,
    Count,
    Null
}
public enum ArmorSubElement
{
    CriticalChance, 
    CriticalDamage, 
    HeadShotDamage, 
    Handleing,
    Count,
    Null,
    Unfixed,//지정안됨 랜덤일경우
}
public enum ArmorTelent
{
    Null,
    Telent1,
    Telent2,
    Telent3,
}
public class ItemElement : MonoBehaviour
{
    
}
