using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Inventory : MonoBehaviour
{
    public GameObject weaponPanelPrefab;
    public GameObject weaponList;
    public List<GameObject> weaponPanels;
    public TMP_Text curWeaponNameText;
    public Image curWeaponImage;
    public UserWeapon selectWeapon;
    public void UpdateCanvas()
    {
        
        //for (int i = 0; i < weaponPanels.Count; i++)
        //{
        //    selectWeapon = weaponPanels[i].GetComponent<GearPanel>().OnClicked();
        //}
        UserWeapon selectWeapon = UserManager.Instance.GetCurrentUserWeapon();
        //UserManager.Instance.GetCurrentUserWeapon();
        WeaponData weapnData = Resources.Load<WeaponData>("WeaponData/"+ selectWeapon.key);
        curWeaponNameText.text = weapnData.weaponName;
        curWeaponImage.sprite = weapnData.sprite;
    }
    private void OnEnable()
    {
        UpdateCanvas();
        for (int i = 0; i < weaponPanels.Count; i++)
        {
            Destroy(weaponPanels[i]);
        }
        weaponPanels.Clear();
        
        for (int i = 0; i < UserManager.Instance.userData.userWeapons.Count; i++)
        {
            GameObject panel = Instantiate(weaponPanelPrefab, weaponList.transform);
            string key = UserManager.Instance.userData.userWeapons[i].key;
            panel.GetComponent<GearPanel>().SetData(key);
            weaponPanels.Add(panel);
            
        }
           
    }
}
