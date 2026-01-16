using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class WeaponPanel : GearPanel
{
    public UserWeapon userWeapon;
    public WeaponData weaponData;
    public WeaponEquipSlot weaponEquipSlot;
    public WeaponSlotType weaponSlotType;

    public override void Awake()
    {
        base.Awake();
        image = transform.Find("GPImage").GetComponentInChildren<Image>();
        text = transform.Find("GPWeaponName").GetComponentInChildren<TMP_Text>();
    }
    public override void SetData(UserWeapon userWeapon)
    {
        weaponData = Resources.Load<WeaponData>("WeaponData/" + userWeapon.key);
        text.text = weaponData.weaponName;
        image.sprite = weaponData.sprite;
        weaponSlotType = weaponData.weaponSlotType;


        this.userWeapon = userWeapon;
        if (userWeapon.weaponEuiped == true)
        {
            image.color = Color.white;
        }
        else
        {
            image.color = Color.black;
        }
    }
    public override void OnClicked()
    {
        //1번클릭했을시 선택상태
        if (select == false)
        {
            select = true;
            GetComponentInParent<WeaponInventory>().uWeapon = userWeapon;
            GetComponentInParent<WeaponInventory>().WeaponSelected(userWeapon);
        }
    }
    public void OnClickedRemove()
    {
        Debug.Log("작동됨");
        if (userWeapon.weaponEuiped == false)
        {
            UserManager.instance.RemoveWeapon(userWeapon);
            Destroy(this.gameObject);
        }
        
    }

    public void UpdatePanel()
    {
       if(userWeapon == GetComponentInParent<WeaponInventory>().uWeapon)
       {
            select = true;
       }
       else
       {
            select = false;
       }
    }
}
