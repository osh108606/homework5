using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    public static UserManager instance;
    public UserData userData;
    public void Awake()
    {
        instance = this;
        userData = SaveManager.LoadData<UserData>("UserData.json"); //저장된 데이터 불러오기
        if (userData == null)// 저장된 데이터가 없을경우
        {
            userData = new UserData();//새 데이터 생성

            //기본아이템 지급
            //*무기*
            UserWeapon userWeapon1 = new UserWeapon();//1번무기
            userWeapon1.weaponDraw = true;
            userWeapon1.weaponEuiped = true;
            userWeapon1.key = "M4";
            userWeapon1.weaponEquipSlot = WeaponEquipSlot.main1;
            userWeapon1.weaponData = Resources.Load<WeaponData>("WeaponData/" + userWeapon1.key);
            userData.userWeapons.Add(userWeapon1);


            UserWeapon userWeapon2 = new UserWeapon();//2번무기
            userWeapon2.weaponDraw = false;
            userWeapon2.weaponEuiped = true;
            userWeapon2.key = "MP5";
            userWeapon2.weaponEquipSlot = WeaponEquipSlot.main2;
            userWeapon2.weaponData = Resources.Load<WeaponData>("WeaponData/" + userWeapon2.key);
            userData.userWeapons.Add(userWeapon2);
            

            UserWeapon userWeapon3 = new UserWeapon();//3번무기
            userWeapon3.weaponDraw = false;
            userWeapon3.weaponEuiped = true;
            userWeapon3.key = "Glock";
            userWeapon3.weaponEquipSlot = WeaponEquipSlot.sub;
            userWeapon3.weaponData = Resources.Load<WeaponData>("WeaponData/" + userWeapon3.key);
            userData.userWeapons.Add(userWeapon3);
            

            UserWeapon userWeapon4 = new UserWeapon();//4번무기
            userWeapon4.weaponDraw = false;
            userWeapon4.weaponEuiped = true;
            userWeapon4.key = "grenadelauncher";
            userWeapon4.weaponEquipSlot = WeaponEquipSlot.special;
            userWeapon4.weaponData = Resources.Load<WeaponData>("WeaponData/" + userWeapon4.key);
            userData.userWeapons.Add(userWeapon4);


            //*장비*
            UserAmmor userAmmor = new UserAmmor();
            userAmmor.ammorEuiped = true;
            userAmmor.key = "Ammor1-1";
            userData.userAmmors.Add(userAmmor);


            //*총알*
            {
                UserAmmo userAmmo0 = new UserAmmo();
                WeaponType type0 = WeaponType.HG;
                userAmmo0.count = 900;
                userAmmo0.weapontype = type0;
                userData.userAmmos.Add(userAmmo0);

                UserAmmo userAmmo1 = new UserAmmo();
                WeaponType type1 = WeaponType.AR;
                userAmmo1.count = 810;
                userAmmo1.weapontype = type1;
                userData.userAmmos.Add(userAmmo1);

                UserAmmo userAmmo2 = new UserAmmo();
                WeaponType type2 = WeaponType.SMG;
                userAmmo2.count = 900;
                userAmmo2.weapontype = type2;
                userData.userAmmos.Add(userAmmo2);

                UserAmmo userAmmo3 = new UserAmmo();
                WeaponType type3 = WeaponType.MG;
                userAmmo3.count = 900;
                userAmmo3.weapontype = type3;
                userData.userAmmos.Add(userAmmo3);

                UserAmmo userAmmo4 = new UserAmmo();
                WeaponType type4 = WeaponType.RF;
                userAmmo4.count = 420;
                userAmmo4.weapontype = type4;
                userData.userAmmos.Add(userAmmo4);

                UserAmmo userAmmo5 = new UserAmmo();
                WeaponType type5 = WeaponType.SR;
                userAmmo5.count = 120;
                userAmmo5.weapontype = type5;
                userData.userAmmos.Add(userAmmo5);

                UserAmmo userAmmo6 = new UserAmmo();
                WeaponType type6 = WeaponType.SG;
                userAmmo6.count = 144;
                userAmmo6.weapontype = type6;
                userData.userAmmos.Add(userAmmo6);

                UserAmmo userAmmo7 = new UserAmmo();
                WeaponType type7 = WeaponType.SP;
                userAmmo7.count = 24;
                userAmmo7.weapontype = type7;
                userData.userAmmos.Add(userAmmo7);
            }

            //Player.instance.currentWeapon = Player.instance.slots[0].weapon;
            //저장
            SaveManager.SaveData("UserData.json", userData);
        }
    }
    public void Relord(Weapon weapon ,UserWeapon userWeapon)
    {
        if (weapon.userWeapon.ammoCount < 0)
            return;
        UserAmmo userAmmo = weapon.userAmmo;
        int reloadCount = weapon.weaponData.maxAmmo;
        int remain = userAmmo.count - weapon.weaponData.maxAmmo;
        if(remain<0)
        {
            reloadCount = userAmmo.count;
        }
        userAmmo.count -= reloadCount;
        userWeapon.ammoCount = reloadCount;

        SaveManager.SaveData("UserData.json", userData);
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

    public void AddWeapon(string key)
    {
        UserWeapon userWeapon = new UserWeapon();
        userWeapon.key = key;
        userWeapon.weaponEuiped = false;
        userData.userWeapons.Add(userWeapon);
    }
    public void Addammor(string key)
    {
        UserAmmor userAmmor = new UserAmmor();
        userAmmor.key = key;
        userAmmor.ammorEuiped = false;
        userData.userAmmors.Add(userAmmor);
    }
    // 기존아이템 장착 비활성화 새무기 장착
    public void ChangeWeapon(string key)//무기
    {
        UserWeapon preUserWeapon = GetEuipedUserWeapon();
        if (preUserWeapon != null)
        {
            preUserWeapon.weaponEuiped = false;
            preUserWeapon.weaponDraw = false;
        }
            
        UserWeapon userWeapon = GetUserWeapon(key);
        userWeapon.weaponEuiped = true;


        SaveManager.SaveData("UserData.json", userData);
    }
    // 장착중인 무기중 들고있는거 변경
    public void ChangeDrawWeapon(string key)
    {
        UserWeapon preUserWeapon = GetDrawUserWeapon();//기존에 들고있던 무기
        if (preUserWeapon != null)
            preUserWeapon.weaponDraw = false;

        UserWeapon userWeapon = GetEuipedUserWeapon(key);//장착중 들게만들 무기
        userWeapon.weaponDraw = true;


        SaveManager.SaveData("UserData.json", userData);
    }

    public void ChangeAmmor(string key)//장비
    {
        UserAmmor preUserAmmor = GetUserAmmor();
        if (preUserAmmor != null)
            preUserAmmor.ammorEuiped = false;

        UserAmmor userAmmor = GetUserAmmor(key);
        userAmmor.ammorEuiped = true;


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

    public UserWeapon GetEquipUserWeapon(WeaponEquipSlot slot)
    {
        for(int i = 0; i< userData.userWeapons.Count; i++)
        {
            if (userData.userWeapons[i].weaponEuiped == true)
            {
                if(userData.userWeapons[i].weaponEquipSlot == slot)
                {
                    return userData.userWeapons[i];
                }
            }
        }
        return null;
    }

    public UserAmmor GetEquipUserAmmor(AmmorEquipSlot slot)
    {
        for (int i = 0; i < userData.userAmmors.Count; i++)
        {
            if (userData.userAmmors[i].ammorEuiped == true)
            {
                if (userData.userAmmors[i].ammorEquipSlot == slot)
                {
                    return userData.userAmmors[i];
                }
            }
        }
        return null;
    }

    public UserAmmor GetUserAmmor(string key)//장비
    {
        for (int i = 0; i < userData.userAmmors.Count; i++)
        {
            if (userData.userWeapons[i].key == key)
            {
                return userData.userAmmors[i];

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

    //현재 들고있는 무기 상태를 반환
    public UserWeapon GetDrawUserWeapon()//1개
    {
        for (int i = 0; i < userData.userWeapons.Count; i++)
        {
            if (userData.userWeapons[i].weaponDraw == true)
            {
                return userData.userWeapons[i];
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
 
    public UserWeapon GetEuipedUserWeapon(string key)//1개
    {
        for (int i = 0; i < userData.userWeapons.Count; i++)
        {
            if (userData.userWeapons[i].weaponEuiped == true && userData.userWeapons[i].key == key)
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

    public UserAmmor GetUserAmmor()//장비
    {
        for (int i = 0; i < userData.userAmmors.Count; i++)
        {
            if (userData.userAmmors[i].ammorEuiped == true)
            {
                return userData.userAmmors[i];
            }
        }
        return null;
    }

    public void Save()
    { 
        SaveManager.SaveData("UserData.json", userData);
    }

    public UserAmmo GetUserAmmo(WeaponType weaponType)
    {
        for(int i = 0; i<userData.userAmmos.Count;i++ )
        {
            if (userData.userAmmos[i].weapontype == weaponType)
            {
                return userData.userAmmos[i];
            }
        }
        
        return null;
    }
    //아이템 제거
    public void RemoveWeapon(UserWeapon userWeapon)//무기
    {
        userData.userWeapons.Remove(userWeapon);
    }
    public void RemoveAmmor(UserAmmor userAmmor)//장비
    {
        userData.userAmmors.Remove(userAmmor);
    }

    public UserDungeon GetUserDungeon(string key)
    {
        for (int i = 0; i < userData.userDungeons.Count; i++)
        {
            if (userData.userDungeons[i].key == key)
            { 
                return userData.userDungeons[i];
            }
        }

        UserDungeon userDungeon = new UserDungeon();
        userDungeon.key = key;
        userData.userDungeons.Add(userDungeon);
        return userDungeon;
    }

    public void ClearDungeon(string key)
    {
        GetUserDungeon(key).clearCount++;
        Save();
    }

}



[System.Serializable]
public class UserData
{
    public List<UserWeapon> userWeapons = new List<UserWeapon>();//무기
    public List<UserAmmo> userAmmos = new List<UserAmmo>();//총알
    public List<UserAmmor> userAmmors = new List<UserAmmor>();//갑옷

   
    public List<UserItem> userItems = new List<UserItem>();// 기타모든 아이템


    public List<UserDungeon> userDungeons = new List<UserDungeon>();// 던전 클리어 정보
}

[System.Serializable]
public class UserAmmo
{
    public WeaponType weapontype;
    public string key;
    public int count;
}

[System.Serializable]
public class UserWeapon
{
    public string key;
    public bool weaponEuiped; //장착중인지
    public bool weaponDraw; //들고있는지
    public WeaponEquipSlot weaponEquipSlot;
    public WeaponData weaponData;
    public int ammoCount; //현제 장착중인 총의 총알갯수
}
[System.Serializable]
public class UserAmmor
{
    public string key;
    public bool ammorEuiped; //장착중인지
    public AmmorEquipSlot ammorEquipSlot;
    public AmmorData ammorData;
}
[System.Serializable]
public class UserItem
{
    public string key;
    public bool itemEuiped;
    public int count;
}

[System.Serializable]
public class UserDungeon
{
    public string key;
    public int clearCount;
    public int tryCount;
    public int killCount;

}