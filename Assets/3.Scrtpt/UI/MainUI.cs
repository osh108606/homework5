using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MainUI : MonoBehaviour
{
    public static MainUI instance;
    public TMP_Text ammoStat;
    public Image reloadImage;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        instance = this;
    }

    
    // Update is called once per frame
    void Update()
    {
        ammoStat.text = $"{Player.instance.currentWeapon.currentAmmo}/{Player.instance.currentWeapon.weaponData.maxAmmo}";

        if (Player.instance.currentWeapon.reLoading)
        {
            ReloadFill();
        }
    }
    public void ReloadFill()
    {
        reloadImage.fillAmount = Player.instance.currentWeapon.reloadTimer / Player.instance.currentWeapon.maxReloadTime;
    }

}
