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
public enum WeaponTypeDamage
{//max15%
    AR,
    SMG,
    MG,
    RF,
    SR,
    SG,
    HG,
    SP,
    Count
}

public enum WeaponSubElement
{
    HPDamage, //AR
    CriticalChance, //SMG
    UnCoverDamage, //MG
    CriticalDamage, //RF
    HeadShotDamage, //SR 
    APDamage, //SG
    Accuracy, // random
    Recoil,
    Count,
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

public enum AmmorMainElement
{
    
    Attack,
    Defense,
    Skill,
    Count,
    Null
}
public enum AmmorSubElement
{
    CriticalDamage,
    CriticalChance,
    HeadShotDamage,
    Count,
    Null,
    Unfixed,//지정안됨 랜덤일경우
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
    
}
