using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    public float damage;
    public float atkSpeed;
    public int maxAmmo;
    public Bullet bulletPrefab;
    public Sprite sprite;
    public string weaponName;
}
