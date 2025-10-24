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
    // �����Ŵ����� ������ ���������� ���ⵥ���ͱ����ڸ� �ް� �װ��� �ݿ��Ͽ� ��������
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
        //1��Ŭ�������� ���û���
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
