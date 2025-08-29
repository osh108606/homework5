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
        if(userData == null)
        {
            userData = new UserData();
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
    

    public void Shooted()
    {
        userData.userAmmos[Player.instance.currentWeapon.idx].count = Player.instance.currentWeapon.currentAmmo;
        SaveManager.SaveData("UserData.json", userData);
    }
}
[System.Serializable]
public class UserData
{
    public List<UserAmmo> userAmmos = new List<UserAmmo>();
}

[System.Serializable]
public class UserAmmo
{
    public string key;
    public int count;
}