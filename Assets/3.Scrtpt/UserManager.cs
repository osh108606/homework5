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
        userData = SaveManager.LoadData<UserData>("UserData.json"); //저장된 데이터 불러오기
        if (userData == null)// 저장된 데이터가 없을경우
        {   
            userData = new UserData();//새 데이터 생성

            //기본아이템 지급
            //*무기*
            UserWeapon userWeapon1 = new UserWeapon();//1번무기
            userWeapon1.weaponDraw = true;
            userWeapon1.weaponEuiped = true;
            userWeapon1.key = "Weapon2";
            userWeapon1.weaponData = Resources.Load<WeaponData>("WeaponData/" + userWeapon1.key);
            userData.userWeapons.Add(userWeapon1);

            UserWeapon userWeapon2 = new UserWeapon();//2번무기
            userWeapon2.weaponDraw = false;
            userWeapon2.weaponEuiped = true;
            userWeapon2.key = "Weapon5";
            userWeapon2.weaponData = Resources.Load<WeaponData>("WeaponData/" + userWeapon2.key);
            userData.userWeapons.Add(userWeapon2);

            UserWeapon userWeapon3 = new UserWeapon();//3번무기
            userWeapon3.weaponDraw = false;
            userWeapon3.weaponEuiped = true;
            userWeapon3.key = "Weapon1";
            userWeapon3.weaponData = Resources.Load<WeaponData>("WeaponData/" + userWeapon3.key);
            userData.userWeapons.Add(userWeapon3);

            UserWeapon userWeapon4= new UserWeapon();//4번무기
            userWeapon4.weaponDraw = false;
            userWeapon4.weaponEuiped = true;
            userWeapon4.key = "Weapon4";
            userWeapon4.weaponData = Resources.Load<WeaponData>("WeaponData/" + userWeapon4.key);
            userData.userWeapons.Add(userWeapon4);

            //*장비*
            UserEquipment userEquipment = new UserEquipment();
            userEquipment.equipmentEuiped = true;
            userEquipment.key = "Equipment1-1";
            userData.userEquipments.Add(userEquipment);
            

            //*총알*
            for (int i = 0; i < userData.userWeapons.Count; i++)//조건문 무기종류갯수만큼으로 변경예정
            {
                UserAmmo userAmmo = new UserAmmo();
                int maxAmmo = userData.userWeapons[i].weaponData.maxAmmo;
                string key = userData.userWeapons[i].key;
                userAmmo.count = maxAmmo;
                userAmmo.key = key;
                userData.userAmmos.Add(userAmmo);

            }
            Player.instance.currentWeapon = Player.instance.weaponSlots[0].GetComponent<Weapon>(); 
            //저장
            SaveManager.SaveData("UserData.json", userData);
        }

    }
    //아이템 추가
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
    
    // 기존아이템 장착 비활성화 새무기 장착
    public void ChangeWeapon(string key)//무기
    {
        UserWeapon preUserWeapon = GetEuipedUserWeapon();
        if (preUserWeapon != null)
            preUserWeapon.weaponEuiped = false;

        UserWeapon userWeapon = GetUserWeapon(key);
        userWeapon.weaponEuiped = true;


        SaveManager.SaveData("UserData.json", userData);
    }
    // 장착중인 무기중 들고있는거 변경
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

    public void ChangeEquipment(string key)//장비
    {
        UserEquipment preUserEquipment = GetUserEquipment();
        if (preUserEquipment != null)
            preUserEquipment.equipmentEuiped = false;

        UserEquipment userEquipment = GetUserEquipment(key);
        userEquipment.equipmentEuiped = true;


        SaveManager.SaveData("UserData.json", userData);
    }

    // 특정유저아이템 반환
    public UserWeapon GetUserWeapon(string key)//무기
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
    public UserEquipment GetUserEquipment(string key)//장비
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

    public UserItem GetUserItem(string key)//아이템
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

    //현재 장착중인 아이템의 상태를 반환
    //*무기*
    public UserWeapon GetEuipedUserWeapon()//1개
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

    public UserWeapon GetEuipedUserWeapons(int index)//여러개
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

    public UserEquipment GetUserEquipment()//장비
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
    //아이템 제거
    public void RemoveWeapon(string key)//무기
    {
        UserWeapon deleteWeapon = GetUserWeapon(key);
        userData.userWeapons.Remove(deleteWeapon);
    }
    public void RemoveEquipment(string key)//장비
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
    public bool weaponEuiped; //장착중인지
    public bool weaponDraw; //들고있는지
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