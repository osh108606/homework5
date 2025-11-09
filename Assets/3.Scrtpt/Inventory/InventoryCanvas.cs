using UnityEngine;

public class InventoryCanvas : MonoBehaviour
{
    private static InventoryCanvas instance;
    public static InventoryCanvas Instance
    {
        get 
        { 
            if(instance == null)
            {
                instance = FindFirstObjectByType<InventoryCanvas>(FindObjectsInactive.Include);

            }
            return instance; 
        }
    }
    public MainInventory mainInventory;
    public WeaponInventory weaponInventory;
    public AmmorInventory ammorInventory;
    public void Awake()
    {
        mainInventory = GetComponentInChildren<MainInventory>(true);
        weaponInventory = GetComponentInChildren<WeaponInventory>(true);
        ammorInventory = GetComponentInChildren<AmmorInventory>(true);
    }

    public void OpenMainInventory()
    {
        
        if (mainInventory.gameObject.activeSelf == true)
        {
            mainInventory.Close();
        }
        else
        {
            ammorInventory.Close();
            weaponInventory.Close();
            mainInventory.Open();
        }
    }
    public void OpenWeaponInventory(WeaponEquipSlot weaponEquipSlot, WeaponSlotType weaponSlotType)
    { 
        mainInventory.Close();
        weaponInventory.Open(weaponEquipSlot, weaponSlotType); 
    }

    public void OpenAmmorInventory(AmmorEquipSlot ammorEquipSlot)
    {
        mainInventory.Close();
        ammorInventory.Open(ammorEquipSlot);
    }
}
