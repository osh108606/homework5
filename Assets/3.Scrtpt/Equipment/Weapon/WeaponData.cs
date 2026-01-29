using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string key;//총기 키값
    public int idx;//총기 고유 번호
    public string weaponName;//총기 이름
    public string weaponBase;//총기 베이스

    public float damage;//총기 데미지
    public float rpm;//공격 속도
    public float reloadTime;//장전속도
    public float spreadRange;// 발사 범위 명중율의 값만큼 감소됨
    public float accuracy;//명중율
    public float stability;//반동
        
    public int maxAmmo;//최대 탄창
    public int pellets;//한발에 나가는 총알 수
    public bool auto;//자동사격 여부
    public bool spreadShot;//산탄 여부

    public Sprite sprite;
    public Bullet bulletPrefab;
    public WeaponType weaponType;//무기 종류
    public WeaponSlotType weaponSlotType;//무기 장착슬롯
    public WeaponAbility weaponAbility;
}
