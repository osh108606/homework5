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
        ammoStat= GetComponentInChildren<TMP_Text>();
    }

    
    void Update()
    {
        if (Player.Instance.currentWeapon != null &&  Player.Instance.currentWeapon.weaponData != null && Player.Instance.currentWeapon.weaponData.weaponType != WeaponType.HG)
        {
            ammoStat.text = $"{Player.Instance.currentWeapon.userWeapon.ammoCount}\n{Player.Instance.currentWeapon.userAmmo.count}";
        }
        else if(Player.Instance.currentWeapon != null && Player.Instance.currentWeapon.weaponData != null)
        {
            ammoStat.text = $"{Player.Instance.currentWeapon.userWeapon.ammoCount}\n{"endless"}";
        }

        if (Player.Instance.currentWeapon.reLoading)
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
        reloadImage.fillAmount = Player.Instance.currentWeapon.reloadTimer / Player.Instance.currentWeapon.maxReloadTime;
    }

}
