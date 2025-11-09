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
    public WeaponSlotPanel[] weaponSlotPanels;
    public AmmorInventory ammorInventory;
    public AmmorPanel[] ammorPanels;
    

    private void OnEnable()
    {
        if (weaponInventory == null) 
            weaponInventory = GetComponentInChildren<WeaponInventory>(true);
        if (ammorInventory == null) 
            ammorInventory = GetComponentInChildren<AmmorInventory>(true);
        if (weaponSlotPanels == null || weaponSlotPanels.Length == 0)
            weaponSlotPanels = GetComponentsInChildren<WeaponSlotPanel>(true);
        if (ammorPanels == null || ammorPanels.Length == 0)
            ammorPanels = GetComponentsInChildren<AmmorPanel>(true);
        Refresh();
    }

    public void Start()
    {
        
    }

    public void Refresh()
    {
        for (int i = 0; i < weaponSlotPanels.Length; i++)
        {
            UserWeapon userWeapon = UserManager.instance.GetEquipUserWeapon(weaponSlotPanels[i].weaponEquipSlot);
            weaponSlotPanels[i].SetUserWeapon(userWeapon);
        }
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    { 
        gameObject.SetActive(false); 
    }

    public void OnClick(WeaponSlotType weaponSlotType)
    {
        for (int i = 0; i <= weaponSlotPanels.Length; i++)
        {
            if (weaponSlotPanels[i].weaponSlotType == weaponSlotType)
            {
                InventoryCanvas.Instance.OpenWeaponInventory(weaponSlotPanels[i].weaponEquipSlot, weaponSlotPanels[i].weaponSlotType);
            }
        }
    }
}
