using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string key;//총기 키값
    public int idx;//총기 고유 번호
    public string weaponName;//총기 이름

    public float damage;//총기 데미지
    public float RPM;//공격 속도
    public float reloadTime;//장전속도
    public int maxAmmo;//최대 탄창
    public int Pellets;//한발에 나가는 총알 수
    public bool auto;//자동사격 여부
    public bool SpreadShot;//산탄 여부

    public Sprite sprite;
    public Bullet bulletPrefab;
    public WeaponType weaponType;//무기 종류
    public WeaponSoltType weaponSoltType;//무기 장착슬롯
}
