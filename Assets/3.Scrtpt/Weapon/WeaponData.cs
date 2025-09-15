using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string key;
    public float damage;
    public float atkSpeed;
    public int maxAmmo;
    public bool auto;
    public string weaponName;
    public Sprite sprite;
    public Bullet bulletPrefab;
}
