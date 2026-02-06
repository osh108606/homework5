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
    public float timer = 0;

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
        timer += Time.deltaTime;
    }
    public override void SetData(UserWeapon uWeapon)
    {
        weaponData = Resources.Load<WeaponData>("WeaponData/" + uWeapon.key);
        text.text = weaponData.weaponName;
        image.sprite = weaponData.sprite;
        weaponSlotType = weaponData.weaponSlotType;


        this.userWeapon = uWeapon;
        UpdatePanel();
    }
    public override void OnClicked()
    {
        Debug.Log("WeaponPanel OnClicked()");
        //1번클릭했을시 선택상태
        if (select == false)
        {
            select = true;
            GetComponentInParent<WeaponInventory>().uWeapon = userWeapon;
            GetComponentInParent<WeaponInventory>().WeaponSelected(userWeapon);
        }
        else
        {
            if (timer <= 0.3f)
            {
                Equip();
            }
        }

        timer = 0;

    }
    
    public void Equip()
    {
        Debug.Log("Equip");
        //userWeapon ?? ????
        if (userWeapon.weaponDraw)
        {
            return;
        }
        
        UserManager.instance.EquipWeapon(userWeapon); //?? ????? ???
        Player.Instance.ChangeWeapon(userWeapon,weaponEquipSlot);
        GetComponent<WeaponInventory>().Updateinventory();
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
            backGround.color = Color.black;
       }
       else
       {
            select = false;
            backGround.color = Color.gray;
       }
       
       if (userWeapon.weaponEuiped)
       {
           innerGround.color = Color.white;
       }
       else
       {
           innerGround.color = Color.gray;
       }       
    }
}
