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
            for (int i = 0; i < userData.ammos.Length; i++)
            {
                userData.ammos[i] = Player.instance.weapons[i].weaponData.maxAmmo;
            }
            //ÀúÀå
            SaveManager.SaveData("UserData.json", userData);
        }
        
    }
    

    public void Shooted()
    {
        userData.ammos[Player.instance.currentWeapon.idx] = Player.instance.currentWeapon.currentAmmo;
        SaveManager.SaveData("UserData.json", userData);
    }
}
public class UserData
{
    public int[] ammos = new int[4];
}
