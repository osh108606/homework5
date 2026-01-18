using UnityEngine;
using UnityEngine.UIElements;

public class AmmoBox : DropItem
{
    public WeaponType weaponType;
    public int count;
    
    public void GetAmmo()
    {
        UserManager.instance.AddAmmo(weaponType, count);
    }
}