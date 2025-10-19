using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MainUI : MonoBehaviour
{
    public static MainUI instance;
    public TMP_Text ammoStat;
    public Image reloadImage;
    
    
    private void Awake()
    {
        instance = this;
    }

    
    void Update()
    {
        if (Player.instance.currentWeapon != null &&  Player.instance.currentWeapon.weaponData != null)
        {
            ammoStat.text = $"{Player.instance.currentWeapon.userAmmo.count}/{Player.instance.currentWeapon.weaponData.maxAmmo}";
        }

        if (Player.instance.currentWeapon.reLoading)
        {
            ReloadFill();
        }
        else
        {
            reloadImage.fillAmount = 0;
        }
        
        
    }
    public void ReloadFill()
    {
        reloadImage.fillAmount = Player.instance.currentWeapon.reloadTimer / Player.instance.currentWeapon.maxReloadTime;
    }

}
