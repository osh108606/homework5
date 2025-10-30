using UnityEngine;
public enum WeaponEquipSlot
{
    main1,
    main2,
    sub,
    special
}
public class WeaponSlot : MonoBehaviour
{
    public WeaponEquipSlot weaponEquipSlot;
    public WeaponSoltType weaponSoltType;
    public Weapon weapon;

    public void WeaponEquip()
    {     
        if( weapon != null )
            Destroy( weapon.gameObject );

        UserWeapon userWeapon = UserManager.instance.GetEquipUserWeapon(weaponEquipSlot);
        if (userWeapon == null)
        {
            return;
        }
        Weapon weaponPrefab = Resources.Load<Weapon>( "Weapon/" + userWeapon.key );
        
        weapon = Instantiate(weaponPrefab, transform.position, Quaternion.identity);
        weapon.transform.parent = transform;

    }
    
}
