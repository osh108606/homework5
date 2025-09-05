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
            for (int i = 0; i < Player.instance.weapons.Length; i++)
            {
                UserAmmo userAmmo = new UserAmmo();
                int maxAmmo = Player.instance.weapons[i].weaponData.maxAmmo;
                string key = Player.instance.weapons[i].key;
                userAmmo.count = maxAmmo;
                userAmmo.key = key;
                userData.userAmmos.Add(userAmmo);

            }
            //ÀúÀå
            SaveManager.SaveData("UserData.json", userData);
        }

    }

    public void WeaponHandle()
    {
        SaveManager.SaveData("UserData.json", userData);
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