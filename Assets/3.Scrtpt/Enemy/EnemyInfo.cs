using UnityEngine;

[CreateAssetMenu(fileName = "EnemyInfo", menuName = "Scriptable Objects/EnemyInfo")]
public class EnemyInfo : ScriptableObject
{
    public EnemyType enemyType; // 적 타입
    public Enemyfaction enemyFaction; // 적 진형
    public float Maxhp; // 최대체력
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

public enum Enemyfaction //적 진형
{

    faction1,
    faction2,
    faction3,
    faction4,
    faction5,
    faction6

}