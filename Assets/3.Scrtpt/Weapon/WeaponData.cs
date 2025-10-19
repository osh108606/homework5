using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string key;//�ѱ� Ű��
    public int idx;//�ѱ� ���� ��ȣ
    public string weaponName;//�ѱ� �̸�

    public float damage;//�ѱ� ������
    public float atkSpeed;//���� �ӵ�
    public int maxAmmo;//�ִ� źâ
    public int Pellets;//�ѹ߿� ������ �Ѿ� ��
    public bool auto;//�ڵ���� ����
    public bool SpreadShot;//��ź ����

    public Sprite sprite;
    public Bullet bulletPrefab;
}
