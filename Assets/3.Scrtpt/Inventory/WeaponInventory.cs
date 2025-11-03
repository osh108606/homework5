using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInventory : SubInventory
{
    public GameObject weaponPanelPrefab;
    public GridLayoutGroup weaponList;
    public List<GameObject> weaponPanels;
    public override void Awake()
    {
        base.Awake();
        weaponList = GetComponentInChildren<GridLayoutGroup>();
    }

    public override void UpdateCanvas()
    {

        //for (int i = 0; i < weaponPanels.Count; i++)
        //{
        //    selectWeapon = weaponPanels[i].GetComponent<GearPanel>().OnClicked();
        //}
        UserWeapon selectWeapon = UserManager.instance.GetEuipedUserWeapon();
        //UserManager.Instance.GetCurrentUserWeapon();
        WeaponData weapnData = Resources.Load<WeaponData>("WeaponData/" + selectWeapon.key);
        curEuiptmentNameText.text = weapnData.weaponName;
        curEuiptmentImage.sprite = weapnData.sprite;
    }

    private void OnEnable()
    {
        UpdateCanvas();
        for (int i = 0; i < weaponPanels.Count; i++)
        {
            Destroy(weaponPanels[i]);
        }
        weaponPanels.Clear();

        for (int i = 0; i < UserManager.instance.userData.userWeapons.Count; i++)
        {
            GameObject panel = Instantiate(weaponPanelPrefab, weaponList.transform);
            UserWeapon userWeapon = UserManager.instance.userData.userWeapons[i];
            panel.GetComponent<WeaponPanel>().SetData(userWeapon);
            weaponPanels.Add(panel);

        }
        WeaponSelected(null);
    }
    public void WeaponSelected(WeaponData weaponData)
    {
        if (weaponData == null)
        {
            curEuiptmentNameText.enabled = false;
            curEuiptmentImage.enabled = false;
            return;
        }

        UserManager.instance.ChangeWeapon(weaponData.key);
        Player.instance.ChangeWeapon(weaponData.key);



        curEuiptmentNameText.enabled = true;
        curEuiptmentImage.enabled = true;

        curEuiptmentNameText.text = weaponData.weaponName;
        curEuiptmentImage.sprite = weaponData.sprite;
    }
}
