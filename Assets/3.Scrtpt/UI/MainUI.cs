using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MainUI : MonoBehaviour
{
    public static MainUI instance;
    public TMP_Text ammoStat;
    public Image reloadImage;
    public GameObject inventory;
    private bool inventoryState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        instance = this;
    }

    void Start() 
    {
        inventoryState = true; 
    }
   
    // Update is called once per frame
    void Update()
    {
        ammoStat.text = $"{Player.instance.currentWeapon.userAmmo.count}/{Player.instance.currentWeapon.weaponData.maxAmmo}";

        if (Player.instance.currentWeapon.reLoading)
        {
            ReloadFill();
        }
        else
        {
            reloadImage.fillAmount = 0;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryState == true)
            {
                inventory.SetActive(false);
                inventoryState = false; 
            } 
            else 
            {
                inventory.SetActive(true);
                inventoryState = true;
            }
        
        }
    }
    public void ReloadFill()
    {
        reloadImage.fillAmount = Player.instance.currentWeapon.reloadTimer / Player.instance.currentWeapon.maxReloadTime;
    }

}
