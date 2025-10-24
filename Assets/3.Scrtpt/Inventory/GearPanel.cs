using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GearPanel : MonoBehaviour
{
    public Image image;
    public TMP_Text text;
    public UserWeapon userWeapon;
    public string key;
    public bool select = false;
    public WeaponData weaponData;
    // 유저매니저로 보유한 무기정보의 무기데이터구분자를 받고 그것을 반영하여 내보낸다
    public void SetData(string key)
    {
        this.key = key;
        weaponData = Resources.Load<WeaponData>("WeaponData/" + key);
        text.text = weaponData.weaponName;
        image.sprite = weaponData.sprite;
       


        userWeapon = UserManager.instance.GetUserWeapon(key);
        if (userWeapon.weaponEuiped == true)
        {
            image.color = Color.white;
        }
        else
        {
            image.color = Color.black;
        }
    }
    public void OnClicked()
    {
        //1번클릭했을시 선택상태
        if (select == false)
        {
            select = true;
            GetComponentInParent<Inventory>().Selected(weaponData);
        } 
        else
        {
            select = false;
            GetComponentInParent<Inventory>().Selected(null);
        }
        
    }
    public void OnClickedRemove()
    {
        if(userWeapon.weaponEuiped == false)
        {
            UserManager.instance.RemoveWeapon(key);
            Destroy(this.gameObject);
        }
        else
        {

        }
        
        
    }
}
