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
        if (userData == null)
        {   
            userData = new UserData();
            UserWeapon userWeapon = new UserWeapon();
            userWeapon.weaponEuiped = true;
            userWeapon.key = "Weapon1";
            userData.userWeapons.Add(userWeapon);
            
            Player.instance.currentWeapon = Player.instance.weapons[0].GetComponent<Weapon>(); 
            for (int i = 0; i < Player.instance.weapons.Length; i++)
            {
                UserAmmo userAmmo = new UserAmmo();
                int maxAmmo = Player.instance.weapons[i].weaponData.maxAmmo;
                string key = Player.instance.weapons[i].key;
                userAmmo.count = maxAmmo;
                userAmmo.key = key;
                userData.userAmmos.Add(userAmmo);

            }
            //저장
            SaveManager.SaveData("UserData.json", userData);
        }

    }
    
    // 기존무기 장착 비활성화 새무기 장착
    public void ChangeWeapon(string key)
    {
        UserWeapon preUserWeapon = GetCurrentUserWeapon();
        if (preUserWeapon != null)
            preUserWeapon.weaponEuiped = false;

        UserWeapon userWeapon = GetUserWeapon(key);
        userWeapon.weaponEuiped = true;


        SaveManager.SaveData("UserData.json", userData);
    }

    // 특정유저무기 반환
    public UserWeapon GetUserWeapon(string key)
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

    //현재 장착중인 무기의 상태를 반환
    public UserWeapon GetCurrentUserWeapon()
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
    public void RemoveWeapon(string key)
    {
        UserWeapon deleteWeapon = GetUserWeapon(key);

        userData.userWeapons.Remove(deleteWeapon);
    }
}



[System.Serializable]
public class UserData
{
    public List<UserWeapon> userWeapons = new List<UserWeapon>();
    public List<UserAmmo> userAmmos = new List<UserAmmo>();
    
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