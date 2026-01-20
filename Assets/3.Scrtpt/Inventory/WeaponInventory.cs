using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInventory : SubInventory
{
    public WeaponPanel weaponPanelPrefab;
    public GridLayoutGroup weaponList;
    public UserWeapon uWeapon;
    public List<WeaponPanel> weaponPanels;
    public WeaponEquipSlot weaponEquipSlot;
    public WeaponSlotType weaponSlotType;

    public override void OnEnable()
    {
        weaponList = GetComponentInChildren<GridLayoutGroup>();

        curEuiptmentNameText = GetComponentInChildren<TMP_Text>();
    }
    public void Open(WeaponEquipSlot equipSlot, WeaponSlotType slotType)
    {
        weaponEquipSlot = equipSlot;
        weaponSlotType = slotType;
        gameObject.SetActive(true);

        //UpdateCanvas();
        for (int i = 0; i < weaponPanels.Count; i++)
        {
            Destroy(weaponPanels[i].gameObject);           
        }
        weaponPanels.Clear();

        for (int i = 0; i < UserManager.instance.userData.userWeapons.Count; i++)
        {
            string key = UserManager.instance.userData.userWeapons[i].key;
            WeaponData weaponData = Resources.Load<WeaponData>("WeaponData/" + key);
            if (weaponData.weaponSlotType == weaponSlotType)
            {
                WeaponPanel panel = Instantiate(weaponPanelPrefab, weaponList.transform);
                UserWeapon userWeapon = UserManager.instance.userData.userWeapons[i];
                panel.SetData(userWeapon);
                weaponPanels.Add(panel);
            }
        }

        uWeapon = UserManager.instance.GetEuipedUserWeapon(equipSlot);
        WeaponSelected(uWeapon);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void WeaponSelected(UserWeapon userWeapon)
    {
        if (userWeapon == null)
        {
            curEuiptmentNameText.enabled = false;
            curEuiptmentImage.enabled = false;
            return;
        }

        //UserManager.instance.ChangeWeapon(userWeapon.key);
        //Player.Instance.ChangeWeapon(userWeapon.key, false);


        WeaponData weaponData = Resources.Load<WeaponData>($"WeaponData/{userWeapon.key}");
        curEuiptmentNameText.enabled = true;
        curEuiptmentImage.enabled = true;

        curEuiptmentNameText.text = weaponData.weaponName;
        curEuiptmentImage.sprite = weaponData.sprite;


        for (int i = 0; i < weaponPanels.Count; i++)
        {
            weaponPanels[i].UpdatePanel();
        }
    }
}
