using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.Linq;
public class MainInventory : MonoBehaviour
{
    
    
    public UserWeapon selectWeapon;
    public UserAmmor selectAmmor;
    public WeaponInventory weaponInventory;
    public WeaponPanel[] weaponPanels;
    public AmmorInventory ammorInventory;
    public AmmorPanel[] ammorPanels;
    public void Awake()
    {
        weaponInventory = GetComponentInChildren<WeaponInventory>();
        ammorInventory = GetComponentInChildren<AmmorInventory>();
        weaponPanels = GetComponentsInChildren<WeaponPanel>();
        ammorPanels = GetComponentsInChildren<AmmorPanel>();
    }

    public void OnClick()
    {
        for(int i = 0; i <= weaponPanels.Length; i++)
        {
            if(weaponPanels[i].weaponSoltType == WeaponSoltType.Main)
            {

            }
            else if(weaponPanels[i].weaponSoltType == WeaponSoltType.Sub)
            {

            }
            else if (weaponPanels[i].weaponSoltType == WeaponSoltType.Special)
            {

            }
        }
    }
}
