using UnityEngine;

[CreateAssetMenu(fileName = "EnemyInfo", menuName = "Scriptable Objects/EnemyInfo")]
public class EnemyInfo : ScriptableObject
{
    public EnemyType enemyType; // �� Ÿ��
    public Enemyfaction enemyfaction; // �� ����
    public float Maxhp; // �ִ�ü��
    public float moveSpeed; // �̵��ӵ�
    public float attackDamage; // ���ݷ�
    public float attackRange; // ���� ����
    
    public float sightRange; //�߰ݹ���
    public float attackDelay; //���ݵ�����
}


public enum EnemyType //�� Ÿ��
{
    Melee,
    Ranged


}

public enum Enemyfaction //�� ����
{

    faction1,
    faction2,
    faction3,
    faction4,
    faction5,
    faction6

}