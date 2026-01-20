using UnityEngine;

[CreateAssetMenu(fileName = "EnemyInfo", menuName = "Scriptable Objects/EnemyInfo")]
public class EnemyInfo : ScriptableObject
{
    public EnemyType enemyType; // 적 타입
    public EnemyFaction enemyFaction; // 적 진형
    public float maxHp; // 최대체력
    public float maxAp;
    public float moveSpeed; // 이동속도
    public float attackDamage; // 공격력
    public float attackRange; // 공격 범위
    
    public float sightRange; //추격범위
    public float attackDelay; //공격딜레이
    public float attackSpeed;
}


public enum EnemyType //적 타입
{
    Melee,
    Ranged
}

public enum EnemyFaction //적 진형
{

    Faction1,
    Faction2,
    Faction3,
    Faction4,
    Faction5,
    Faction6

}