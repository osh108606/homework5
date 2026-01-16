using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    public static UserManager instance;
    public UserData userData;
    
    public void Awake()
    {
        instance = this;
        
        //int saveOrder = TitleSceneManager.instance.LoadGame();
        //string saveName = TitleSceneManager.instance.userSaveData.saveSlots[saveOrder].userDataFileName;
        //userData = SaveManager.LoadData<UserData>(saveName); //저장된 데이터 불러오기
        userData = SaveManager.LoadData<UserData>("UserData.json");
        if (userData != null)
        { 
            for (int i = 0; i< userData.userWeapons.Count;i++)
            {
                if(userData.userWeapons[i].weaponEuiped == true)
                {               
                    for (int j = 0; j < userData.userAmmos.Count; j++)
                    { 
                        if(userData.userAmmos[j].weapontype == userData.userWeapons[i].weaponData.weaponType)
                        {
                            userData.userAmmos[j].count -= (userData.userWeapons[i].weaponData.maxAmmo - userData.userWeapons[i].ammoCount);
                        }
                    }
                    userData.userWeapons[i].ammoCount = userData.userWeapons[i].weaponData.maxAmmo;
                }
            }
        }
        

    }
    public void Start()
    {
        if (userData == null)// 저장된 데이터가 없을경우
        {
            userData = new UserData();//새 데이터 생성
            //기본아이템 지급
            //*무기*
            #region Weapon
            AddDebugWeapon("M4", 0, WeaponEquipSlot.main1,true);//1번무기
            userData.userWeapons[0].ammoCount = userData.userWeapons[0].weaponData.maxAmmo;
            AddDebugWeapon("MP5", 0, WeaponEquipSlot.main2,false);//2번무기
            userData.userWeapons[1].ammoCount = userData.userWeapons[1].weaponData.maxAmmo;
            AddDebugWeapon("Glock", 0, WeaponEquipSlot.sub, false);//3번무기
            userData.userWeapons[2].ammoCount = userData.userWeapons[2].weaponData.maxAmmo;
            AddDebugWeapon("grenadelauncher", 0, WeaponEquipSlot.special, false);//4번무기
            userData.userWeapons[3].ammoCount = userData.userWeapons[3].weaponData.maxAmmo;
            #endregion
            //*장비*
            #region Armor          
            UserArmor userArmor1 = new UserArmor();
            userArmor1.armorEuiped = true;
            userArmor1.key = "Armor1-1";
            userData.userArmors.Add(userArmor1);

            UserArmor userArmor2 = new UserArmor();
            userArmor2.armorEuiped = true;
            userArmor2.key = "Armor1-2";
            userData.userArmors.Add(userArmor2);

            UserArmor userArmor3 = new UserArmor();
            userArmor3.armorEuiped = true;
            userArmor3.key = "Armor1-3";
            userData.userArmors.Add(userArmor3);

            UserArmor userArmor4 = new UserArmor();
            userArmor4.armorEuiped = true;
            userArmor4.key = "Armor1-4";
            userData.userArmors.Add(userArmor4);

            UserArmor userArmor5 = new UserArmor();
            userArmor5.armorEuiped = true;
            userArmor5.key = "Armor1-5";
            userData.userArmors.Add(userArmor5);

            UserArmor userArmor6 = new UserArmor();
            userArmor6.armorEuiped = true;
            userArmor6.key = "Armor1-6";
            userData.userArmors.Add(userArmor6);
            #endregion
            //*총알*
            #region Ammo
            UserAmmo userAmmo0 = new UserAmmo();
            userAmmo0.count = Player.Instance.playerAbility.initHGAmmoLimit;
            userAmmo0.weapontype = WeaponType.HG;
            userData.userAmmos.Add(userAmmo0);

            UserAmmo userAmmo1 = new UserAmmo();
            userAmmo1.count = Player.Instance.playerAbility.initARAmmoLimit;
            userAmmo1.weapontype = WeaponType.AR;
            userData.userAmmos.Add(userAmmo1);

            UserAmmo userAmmo2 = new UserAmmo();
            userAmmo2.count = Player.Instance.playerAbility.initSMGAmmoLimit;
            userAmmo2.weapontype = WeaponType.SMG;
            userData.userAmmos.Add(userAmmo2);

            UserAmmo userAmmo3 = new UserAmmo();
            userAmmo3.count = Player.Instance.playerAbility.initMGAmmoLimit;
            userAmmo3.weapontype = WeaponType.MG;
            userData.userAmmos.Add(userAmmo3);

            UserAmmo userAmmo4 = new UserAmmo();
            userAmmo4.count = Player.Instance.playerAbility.initRFAmmoLimit;
            userAmmo4.weapontype = WeaponType.RF;
            userData.userAmmos.Add(userAmmo4);

            UserAmmo userAmmo5 = new UserAmmo();
            userAmmo5.count = Player.Instance.playerAbility.initSRAmmoLimit;
            userAmmo5.weapontype = WeaponType.SR;
            userData.userAmmos.Add(userAmmo5);

            UserAmmo userAmmo6 = new UserAmmo(); 
            userAmmo6.count = Player.Instance.playerAbility.initSGAmmoLimit;
            userAmmo6.weapontype = WeaponType.SG;
            userData.userAmmos.Add(userAmmo6);

            UserAmmo userAmmo7 = new UserAmmo(); 
            userAmmo7.count = Player.Instance.playerAbility.initSPAmmoLimit;
            userAmmo7.weapontype = WeaponType.SP;
            userData.userAmmos.Add(userAmmo7);
            #endregion
            //저장
            SaveManager.SaveData("UserData.json", userData);
        }
    }
    public void AddDebugWeapon(string key, int grade, WeaponEquipSlot weaponEquipSlot , bool draw)//디버그용
    {
        UserWeapon userWeapon = new UserWeapon();
        userWeapon.key = key;
        userWeapon.weaponEuiped = true;
        if (draw == true)
            userWeapon.weaponDraw = true;
        else
            userWeapon.weaponDraw = false;

        userWeapon.weaponEquipSlot = weaponEquipSlot;
        userWeapon.weaponData = Resources.Load<WeaponData>("WeaponData/" + key);

        userWeapon.weaponAbility = new WeaponAbility();
        userWeapon.weaponAbility.grade = grade;
        userWeapon.weaponAbility.itemGrade = (ItemGrade)grade;

        WeaponTypeElementData elementData = WeaponManager.Instance.GetWeaponTypeElementData(userWeapon.weaponData.weaponType);

        userWeapon.weaponAbility.weaponTypeDamageData.weaponType = elementData.weaponType;
        userWeapon.weaponAbility.weaponTypeDamageData.value = Random.Range(elementData.addWeaponDamageDataValues[grade].x, elementData.addWeaponDamageDataValues[grade].y);
        if(userWeapon.weaponAbility.grade != 0)
        {
            userWeapon.weaponAbility.weaponFixSubElementData.weaponSubElement = elementData.fixWeaponSubElement;
            userWeapon.weaponAbility.weaponFixSubElementData.value = Random.Range(elementData.fixWeaponSubElementValues[grade].x, elementData.fixWeaponSubElementValues[grade].y);
        }
        else
        {
            userWeapon.weaponAbility.weaponFixSubElementData.weaponSubElement = WeaponSubElement.Null;
            userWeapon.weaponAbility.weaponFixSubElementData.value = 0;
        }


        List<WeaponSubElement> elements = new List<WeaponSubElement>();
        for (int i = 0; i < (int)WeaponSubElement.Count; i++)
        {
            elements.Add((WeaponSubElement)i);
        }
        elements.Remove(elementData.fixWeaponSubElement);

        for (int i = 0; i < elementData.randomElementCounts[grade]; i++)
        {
            int randomIdx = Random.Range(0, elements.Count);
            WeaponSubElement subRandomElement = elements[randomIdx];

            WeaponSubElementData weaponRandomSubElementData = new WeaponSubElementData();
            weaponRandomSubElementData.weaponSubElement = subRandomElement;
            RandomWeaponSubElementData randomElementData = WeaponManager.Instance.GetRandomElementData(weaponRandomSubElementData.weaponSubElement);
            weaponRandomSubElementData.value = Random.Range(randomElementData.weaponSubElementValues[grade].x, randomElementData.weaponSubElementValues[grade].y);
            userWeapon.weaponAbility.weaponRandomSubElementDatas.Add(weaponRandomSubElementData);
            elements.Remove(subRandomElement);
        }
        int randomTelIdx = Random.Range(0, (int)WeaponTelent.Count);
        if (userWeapon.weaponAbility.grade == 3)//WeaponTypeDamage & WeaponSubElement & WeaponSubElement(random) & WeaponTelent(random)
        {
            userWeapon.weaponAbility.weaponTelent[0] = (WeaponTelent)randomTelIdx;
        }
        else if (userWeapon.weaponAbility.grade == 4)//WeaponTypeDamage & WeaponSubElement & WeaponSubElement(random) & WeaponTelent(random)
        {
            userWeapon.weaponAbility.weaponTelent[0] = (WeaponTelent)randomTelIdx;
        }
        userData.userWeapons.Add(userWeapon);
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
    
    public void AddWeapon(string key, int grade, DropWeaponItem dropWeaponItem)
    {
        UserWeapon userWeapon = new UserWeapon();

        userWeapon.key = key;
        userWeapon.weaponEuiped = false;
        userWeapon.weaponAbility = dropWeaponItem.weaponAbility;
        userData.userWeapons.Add(userWeapon);
    }
    public void AddArmor(string key, int grade)
    {
        UserArmor userArmor = new UserArmor();

        userArmor.key = key;
        userArmor.armorEuiped = false;
        userArmor.armorAbility = new ArmorAbility();
        userArmor.armorAbility.grade = grade;
        userData.userArmors.Add(userArmor);
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

    public void ChangeArmor(string key)//장비
    {
        UserArmor preUserArmor = GetUserArmor();
        if (preUserArmor != null)
            preUserArmor.armorEuiped = false;

        UserArmor userArmor = GetUserArmor(key);
        userArmor.armorEuiped = true;


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

    public UserArmor GetEquipUserArmor(ArmorEquipSlot slot)
    {
        for (int i = 0; i < userData.userArmors.Count; i++)
        {
            if (userData.userArmors[i].armorEuiped == true)
            {
                if (userData.userArmors[i].armorEquipSlot == slot)
                {
                    return userData.userArmors[i];
                }
            }
        }
        return null;
    }

    public UserArmor GetUserArmor(string key)//장비
    {
        for (int i = 0; i < userData.userArmors.Count; i++)
        {
            if (userData.userArmors[i].key == key)
            {
                return userData.userArmors[i];

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
    public UserWeapon GetEuipedUserWeapon(WeaponEquipSlot weaponEquipSlot)//1개
    {
        for (int i = 0; i < userData.userWeapons.Count; i++)
        {
            if (userData.userWeapons[i].weaponEquipSlot == weaponEquipSlot)
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

    public UserArmor GetUserArmor()//장비
    {
        for (int i = 0; i < userData.userArmors.Count; i++)
        {
            if (userData.userArmors[i].armorEuiped == true)
            {
                return userData.userArmors[i];
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
    public void RemoveArmor(UserArmor userArmor)//장비
    {
        userData.userArmors.Remove(userArmor);
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
    public List<UserArmor> userArmors = new List<UserArmor>();//갑옷

   
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
    public int ammoCount; //현재 장착중인 총의 총알갯수
    
    public WeaponEquipSlot weaponEquipSlot;
    public WeaponData weaponData;
    public WeaponAbility weaponAbility;
}
[System.Serializable]
public class WeaponAbility
{
    public int grade;
    public ItemGrade itemGrade;
    public WeaponTypeDamageData weaponTypeDamageData = new WeaponTypeDamageData();
    public WeaponSubElementData weaponFixSubElementData = new WeaponSubElementData();
    public List<WeaponSubElementData> weaponRandomSubElementDatas = new List<WeaponSubElementData>();
    public List<WeaponTelent> weaponTelent = new List<WeaponTelent>();

    public float GetValue(WeaponSubElement element)
    {
        float value = 0;
        if (element == weaponFixSubElementData.weaponSubElement)
        {
            value += weaponFixSubElementData.value;
        }

        for (int i = 0; i < weaponRandomSubElementDatas.Count; i++)
        {
            if (weaponRandomSubElementDatas[i].weaponSubElement == element)
            {
                value += weaponRandomSubElementDatas[i].value;
            }
        }
        return value;
    }

    public float GetFixValue(WeaponSubElement element)
    {
        if (element == weaponFixSubElementData.weaponSubElement)
        {
            return weaponFixSubElementData.value;
        }
        return 0;
    }
}

[System.Serializable]
public class WeaponTypeDamageData
{
    public WeaponType weaponType;
    public float value;
}

[System.Serializable]
public class WeaponSubElementData
{
    public WeaponSubElement weaponSubElement;
    public float value;
}


[System.Serializable]
public class UserArmor
{
    public string key;
    public bool armorEuiped; //장착중인지
    public ArmorEquipSlot armorEquipSlot;
    public ArmorData armorData;

    public ArmorAbility armorAbility;
}

[System.Serializable]
public class ArmorAbility
{
    public int grade;
    public ItemGrade itemGrade;
    public ArmorMainsElementData armorMainsElementData;
}

[System.Serializable]
public class ArmorMainsElementData
{
    public ArmorMainElement armorMainElement;
    public float value;
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