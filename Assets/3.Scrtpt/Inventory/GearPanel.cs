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
    // �����Ŵ����� ������ ���������� ���ⵥ���ͱ����ڸ� �ް� �װ��� �ݿ��Ͽ� ��������
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
        //1��Ŭ�������� ���û���
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
