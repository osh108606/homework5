using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;


public class WeaponSlotPanel : MonoBehaviour
{
    public WeaponEquipSlot weaponEquipSlot;
    public WeaponSlotType weaponSlotType;
    public WeaponData weaponData;
    public Image image;
    public TMP_Text weaponName;

    public void Awake()
    {
        image = transform.Find("GPInnerGround").Find("GPImage").GetComponentInChildren<Image>();
        weaponName = GetComponentInChildren<TMP_Text>();
    }

    public void SetUserWeapon(UserWeapon userWeapon)
    {
        if(userWeapon == null)
        {
            image.enabled = false;
            weaponName.text = "none";
        }
        else 
        {
            //WeaponPanel SetData
            weaponData = Resources.Load<WeaponData>("WeaponData/" + userWeapon.key);
            image.enabled = true;
            image.sprite = weaponData.sprite;
            weaponName.text = weaponData.weaponName;
            weaponSlotType = weaponData.weaponSlotType;

        }
    }

    public void OnClick()
    {
        InventoryCanvas.Instance.OpenWeaponInventory(weaponEquipSlot, weaponSlotType);
    }

}
