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
    public ArmorInventory armorInventory;
    public void Awake()
    {
        mainInventory = GetComponentInChildren<MainInventory>(true);
        weaponInventory = GetComponentInChildren<WeaponInventory>(true);
        armorInventory = GetComponentInChildren<ArmorInventory>(true);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (mainInventory.gameObject.activeSelf == true)
                mainInventory.Close();

            if (weaponInventory.gameObject.activeSelf == true)
            {
                weaponInventory.Close();
                mainInventory.Open();
            }
            if (armorInventory.gameObject.activeSelf == true)
            {
                armorInventory.Close();
                mainInventory.Open();
            }
        }
    }

    public void OpenMainInventory()
    {
        
        if (mainInventory.gameObject.activeSelf == true)
        {
            mainInventory.Close();
        }
        else
        {
            armorInventory.Close();
            weaponInventory.Close();
            mainInventory.Open();
        }
    }
    public void OpenWeaponInventory(WeaponEquipSlot weaponEquipSlot, WeaponSlotType weaponSlotType)
    { 
        mainInventory.Close();
        weaponInventory.Open(weaponEquipSlot, weaponSlotType); 
    }

    public void OpenArmorInventory(ArmorEquipSlot armorEquipSlot)
    {
        mainInventory.Close();
        armorInventory.Open(armorEquipSlot);
    }
}
