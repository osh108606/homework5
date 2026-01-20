using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class WeaponPanel : GearPanel
{
    public Image backGround;
    public Image innerGround;
    public UserWeapon userWeapon;
    public WeaponData weaponData;    
    public WeaponEquipSlot weaponEquipSlot;
    public WeaponSlotType weaponSlotType;

    public override void Awake()
    {
        base.Awake();
        image = transform.Find("GPInnerGround").Find("GPImage").GetComponentInChildren<Image>();
        backGround = transform.Find("GPBackGround").GetComponentInChildren<Image>();
        innerGround = transform.Find("GPInnerGround").GetComponentInChildren<Image>();
        text = transform.Find("GPWeaponName").GetComponentInChildren<TMP_Text>();
    }

    public void Update()
    {
        if (select == true)
            backGround.color = Color.black;
        else
            backGround.color = Color.gray;

        timer += Time.deltaTime;
    }
    public float timer = 0;
    public override void SetData(UserWeapon userWeapon)
    {
        weaponData = Resources.Load<WeaponData>("WeaponData/" + userWeapon.key);
        text.text = weaponData.weaponName;
        image.sprite = weaponData.sprite;
        weaponSlotType = weaponData.weaponSlotType;


        this.userWeapon = userWeapon;
        if (userWeapon.weaponEuiped == true)
        {
            innerGround.color = Color.white;
        }
        else
        {
            innerGround.color = Color.gray;
        }       
    }
    public override void OnClicked()
    {
        if (select == false)
        {
            select = true;           
            GetComponentInParent<WeaponInventory>().uWeapon = userWeapon;
            GetComponentInParent<WeaponInventory>().WeaponSelected(userWeapon);
        }
        else
        {
            if(timer <= 0.15f)
            {
                //??!
                //1. ??? ??? ?? ?? ??? ???? ??
                //?? ??

                //Player.Instance.ChangeWeapon()
                Equip();
            }
        }

        timer = 0;
    }

    public void Equip()
    {
        //userWeapon ?? ????
        if (userWeapon.weaponDraw)
        {
            return;
        }

        //UserManager.instance.ChangeDrawWeapon(userWeapon); //??
        UserManager.instance.ChangeWeapon(userWeapon, true); //?? ????? ???
    }

    public void OnClickedRemove()
    {
        Debug.Log("??????");
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
