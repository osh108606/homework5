using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    public static UserManager Instance;
    public UserData userData;
    
    public void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        
        userData = SaveManager.LoadData<UserData>("UserData.json");
        if (userData == null)//�� ������ ���� �⺻������ ����
        {   
            userData = new UserData();//����
            UserWeapon userWeapon = new UserWeapon();
            userWeapon.weaponEuiped = true;
            userWeapon.key = "Weapon1";
            userData.userWeapons.Add(userWeapon);

            UserEquipment userEquipment = new UserEquipment();//���
            userEquipment.equipmentEuiped = true;
            userEquipment.key = "Equipment1-1";
            userData.userEquipments.Add(userEquipment);

            for (int i = 0; i < Player.instance.weapons.Length; i++)//�Ѿ�
            {
                UserAmmo userAmmo = new UserAmmo();
                int maxAmmo = Player.instance.weapons[i].weaponData.maxAmmo;
                string key = Player.instance.weapons[i].key;
                userAmmo.count = maxAmmo;
                userAmmo.key = key;
                userData.userAmmos.Add(userAmmo);

            }
            Player.instance.currentWeapon = Player.instance.weapons[0].GetComponent<Weapon>(); 
            //����
            SaveManager.SaveData("UserData.json", userData);
        }

    }
    //������ �߰�
    public void Additem(string key)
    {
        UserItem userItem = GetUserItem(key);

        if(userItem == null)
        {
            userItem = new UserItem();
            userItem.key = key;
            userItem.itemEuiped = false;
            userItem.count = 0;
            userData.userItems.Add(userItem);
        }
        userItem.count += 1;
    }
    
    // ���������� ���� ��Ȱ��ȭ ������ ����
    public void ChangeWeapon(string key)//����
    {
        UserWeapon preUserWeapon = GetCurrentUserWeapon();
        if (preUserWeapon != null)
            preUserWeapon.weaponEuiped = false;

        UserWeapon userWeapon = GetUserWeapon(key);
        userWeapon.weaponEuiped = true;


        SaveManager.SaveData("UserData.json", userData);
    }
    public void ChangeEquipment(string key)//���
    {
        UserEquipment preUserEquipment = GetUserEquipment();
        if (preUserEquipment != null)
            preUserEquipment.equipmentEuiped = false;

        UserEquipment userEquipment = GetUserEquipment(key);
        userEquipment.equipmentEuiped = true;


        SaveManager.SaveData("UserData.json", userData);
    }

    // Ư������������ ��ȯ
    public UserWeapon GetUserWeapon(string key)//����
    {
        for (int i = 0; i < userData.userWeapons.Count; i++)
        {
            if (userData.userWeapons[i].key == key)
            {
                return userData.userWeapons[i];
               
            }
        }
        return null;
    }
    public UserEquipment GetUserEquipment(string key)//���
    {
        for (int i = 0; i < userData.userEquipments.Count; i++)
        {
            if (userData.userWeapons[i].key == key)
            {
                return userData.userEquipments[i];

            }
        }
        return null;
    }

    public UserItem GetUserItem(string key)//������
    {
        for (int i = 0; i < userData.userItems.Count; i++)
        {
            if (userData.userItems[i].key == key)
            {
                return userData.userItems[i];
            }
        }
        return null;
    }

    //���� �������� �������� ���¸� ��ȯ
    public UserWeapon GetCurrentUserWeapon()//����
    {
        for (int i = 0; i < userData.userWeapons.Count; i++)
        {
            if (userData.userWeapons[i].weaponEuiped == true)
            {
                return userData.userWeapons[i];
            }
        }
        return null;
    }
    public UserEquipment GetUserEquipment()//���
    {
        for (int i = 0; i < userData.userEquipments.Count; i++)
        {
            if (userData.userEquipments[i].equipmentEuiped == true)
            {
                return userData.userEquipments[i];
            }
        }
        return null;
    }

    public void Shooted()
    { 
        SaveManager.SaveData("UserData.json", userData);
    }

    public UserAmmo GetUserAmmo(string key)
    {
        for(int i = 0; i<userData.userAmmos.Count;i++ )
        {
            if (userData.userAmmos[i].key == key)
            {
                return userData.userAmmos[i];
            }
        }
        
        return null;
    }
    //������ ����
    public void RemoveWeapon(string key)//����
    {
        UserWeapon deleteWeapon = GetUserWeapon(key);
        userData.userWeapons.Remove(deleteWeapon);
    }
    public void RemoveEquipment(string key)//���
    {
        UserEquipment deleteEquipment = GetUserEquipment(key);
        userData.userEquipments.Remove(deleteEquipment);
    }
}



[System.Serializable]
public class UserData
{
    public List<UserWeapon> userWeapons = new List<UserWeapon>();
    public List<UserAmmo> userAmmos = new List<UserAmmo>();
    public List<UserEquipment> userEquipments = new List<UserEquipment>();
    public List<UserItem> userItems = new List<UserItem>();
}

[System.Serializable]
public class UserAmmo
{
    public string key;
    public int count;
}

[System.Serializable]
public class UserWeapon
{
    public string key;
    public bool weaponEuiped;
    
}
[System.Serializable]
public class UserEquipment
{
    public string key;
    public bool equipmentEuiped;
}
[System.Serializable]
public class UserItem
{
    public string key;
    public bool itemEuiped;
    public int count;
}