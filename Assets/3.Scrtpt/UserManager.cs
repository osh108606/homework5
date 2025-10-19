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
        userData = SaveManager.LoadData<UserData>("UserData.json"); //����� ������ �ҷ�����
        if (userData == null)// ����� �����Ͱ� �������
        {   
            userData = new UserData();//�� ������ ����

            //�⺻������ ����
            //*����*
            UserWeapon userWeapon1 = new UserWeapon();//1������
            userWeapon1.weaponDraw = true;
            userWeapon1.weaponEuiped = true;
            userWeapon1.key = "Weapon2";
            userWeapon1.weaponData = Resources.Load<WeaponData>("WeaponData/" + userWeapon1.key);
            userData.userWeapons.Add(userWeapon1);

            UserWeapon userWeapon2 = new UserWeapon();//2������
            userWeapon2.weaponDraw = false;
            userWeapon2.weaponEuiped = true;
            userWeapon2.key = "Weapon5";
            userWeapon2.weaponData = Resources.Load<WeaponData>("WeaponData/" + userWeapon2.key);
            userData.userWeapons.Add(userWeapon2);

            UserWeapon userWeapon3 = new UserWeapon();//3������
            userWeapon3.weaponDraw = false;
            userWeapon3.weaponEuiped = true;
            userWeapon3.key = "Weapon1";
            userWeapon3.weaponData = Resources.Load<WeaponData>("WeaponData/" + userWeapon3.key);
            userData.userWeapons.Add(userWeapon3);

            UserWeapon userWeapon4= new UserWeapon();//4������
            userWeapon4.weaponDraw = false;
            userWeapon4.weaponEuiped = true;
            userWeapon4.key = "Weapon4";
            userWeapon4.weaponData = Resources.Load<WeaponData>("WeaponData/" + userWeapon4.key);
            userData.userWeapons.Add(userWeapon4);

            //*���*
            UserEquipment userEquipment = new UserEquipment();
            userEquipment.equipmentEuiped = true;
            userEquipment.key = "Equipment1-1";
            userData.userEquipments.Add(userEquipment);
            

            //*�Ѿ�*
            for (int i = 0; i < userData.userWeapons.Count; i++)//���ǹ� ��������������ŭ���� ���濹��
            {
                UserAmmo userAmmo = new UserAmmo();
                int maxAmmo = userData.userWeapons[i].weaponData.maxAmmo;
                string key = userData.userWeapons[i].key;
                userAmmo.count = maxAmmo;
                userAmmo.key = key;
                userData.userAmmos.Add(userAmmo);

            }
            Player.instance.currentWeapon = Player.instance.weaponSlots[0].GetComponent<Weapon>(); 
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
        UserWeapon preUserWeapon = GetEuipedUserWeapon();
        if (preUserWeapon != null)
            preUserWeapon.weaponEuiped = false;

        UserWeapon userWeapon = GetUserWeapon(key);
        userWeapon.weaponEuiped = true;


        SaveManager.SaveData("UserData.json", userData);
    }
    // �������� ������ ����ִ°� ����
    public void ChangeDrawWeapon(string key)
    {
        UserWeapon preUserWeapon = GetEuipedUserWeapon();
        if (preUserWeapon != null)
        {
            preUserWeapon.weaponEuiped = false;
            preUserWeapon.weaponDraw = false;
        }
            

        UserWeapon userWeapon = GetUserWeapon(key);
        userWeapon.weaponEuiped = true;
        userWeapon.weaponDraw = true;


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
    //*����*
    public UserWeapon GetEuipedUserWeapon()//1��
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

    public UserWeapon GetEuipedUserWeapons(int index)//������
    {
        int equippedCount = 0;
        for (int i=0; i < userData.userWeapons.Count; i++)
        {
            if (userData.userWeapons[i].weaponEuiped == true)
            {
                if (equippedCount == index)
                {
                    return userData.userWeapons[i];
                }
                equippedCount++;
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
    public bool weaponEuiped; //����������
    public bool weaponDraw; //����ִ���
    public WeaponData weaponData;
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