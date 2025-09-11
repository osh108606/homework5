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
    // 유저매니저로 보유한 무기정보의 무기데이터구분자를 받고 그것을 반영하여 내보낸다
    public void SetData(string key)
    {
        this.key = key;
        text.text = Resources.Load<WeaponData>("WeaponData/" + key).weaponName;
        image.sprite = Resources.Load<WeaponData>("WeaponData/" + key).sprite;
        
        
        userWeapon = UserManager.Instance.GetUserWeapon(key);
        if (userWeapon.weaponEuiped == true)
        {
            image.color = Color.white;
        }
        else
        {
            image.color = Color.black;
        }
    }
    public UserWeapon OnClicked()
    {
        //1번클릭했을시 선택상태
        if (select == false)
        {
            select = true;
        } 
        else
        {
            select = false;
        }
        return UserManager.Instance.GetUserWeapon(key);
    }
    public void OnClickedRemove()
    {
        UserManager.Instance.RemoveWeapon(key);
        Destroy(this.gameObject);
    }
}
