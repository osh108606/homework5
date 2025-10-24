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
    public bool equip;
    public bool draw;
    public Weapon weapon;

    public void Update()
    {
        for(int i = 0;i< UserManager.instance.userData.userWeapons.Count;i++)
        {
            if(UserManager.instance.userData.userWeapons[i].weaponDraw == true)
            {
                draw = UserManager.instance.userData.userWeapons[i].weaponDraw;
            }
        }
        
    }
    public void WeaponDraw()
    {
        draw = true;
    }

    public void WeaponEquip()
    {
        equip = true;
        weapon = GetComponentInChildren<Weapon>();
    }
    
}
