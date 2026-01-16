using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class WeaponSlotPanel : MonoBehaviour
{
    public WeaponEquipSlot weaponEquipSlot;
    public WeaponSlotType weaponSlotType;
    public WeaponData weaponData;
    public Image thumImage;
    public TMP_Text weaponName;

    public void Awake()
    {
        thumImage =transform.Find("GPImage").GetComponentInChildren<Image>();
        weaponName = GetComponentInChildren<TMP_Text>();
    }

    public void SetUserWeapon(UserWeapon userWeapon)
    {
        if(userWeapon == null)
        {
            thumImage.enabled = false;
            weaponName.text = "none";
        }
        else 
        {
            //WeaponPanel SetData
            weaponData = Resources.Load<WeaponData>("WeaponData/" + userWeapon.key);
            thumImage.enabled = true;
            thumImage.sprite = weaponData.sprite;
            weaponName.text = weaponData.weaponName;
            weaponSlotType = weaponData.weaponSlotType;

        }
    }

    public void OnClick()
    {
        InventoryCanvas.Instance.OpenWeaponInventory(weaponEquipSlot, weaponSlotType);
    }

}
