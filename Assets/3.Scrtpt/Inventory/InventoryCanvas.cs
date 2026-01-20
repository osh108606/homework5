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
    public bool canInteraction;
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
            if (mainInventory.gameObject.activeSelf)
                mainInventory.Close();

            if (weaponInventory.gameObject.activeSelf)
            {
                weaponInventory.Close();
                mainInventory.Open();
            }

            if (armorInventory.gameObject.activeSelf)
            {
                armorInventory.Close();
                mainInventory.Open();
            }
        }
        if(mainInventory.gameObject.activeSelf
            || weaponInventory.gameObject.activeSelf 
            || armorInventory.gameObject.activeSelf)
            canInteraction = false;
        else
            canInteraction = true;
    }

    public void OpenMainInventory()
    {
        
        if (mainInventory.gameObject.activeSelf)
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
