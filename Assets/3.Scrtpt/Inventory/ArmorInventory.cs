using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArmorInventory : SubInventory
{
    public GameObject armorPanelPrefab;
    public GridLayoutGroup armorList;
    public List<GameObject> armorPanels;
    public ArmorEquipSlot armorEquipSlot;

    public override void OnEnable()
    {
        armorList = GetComponentInChildren<GridLayoutGroup>();
        curEuiptmentImage = GetComponentInChildren<Image>();
        curEuiptmentNameText = GetComponentInChildren<TMP_Text>();
    }

    public void Open(ArmorEquipSlot equipSlot)
    {
        armorEquipSlot = equipSlot;
        gameObject.SetActive(true);

        UpdateCanvas();
        for (int i = 0; i < armorPanels.Count; i++)
        {
            Destroy(armorPanels[i]);
        }
        armorPanels.Clear();

        for (int i = 0; i < UserManager.instance.userData.userArmors.Count; i++)
        {
            string key = UserManager.instance.userData.userArmors[i].key;
            ArmorData armorData = Resources.Load<ArmorData>("ArmorData/" + key);
            if (armorData.armorEquipSlot == armorEquipSlot)
            {
                GameObject panel = Instantiate(armorPanelPrefab, armorList.transform);
                UserArmor userArmor = UserManager.instance.userData.userArmors[i];
                panel.GetComponent<ArmorPanel>().SetData(userArmor);
                armorPanels.Add(panel);
            }
        }
        ArmorSelected(null);
    }

    public override void UpdateCanvas()
    {
        UserArmor selectArmor = UserManager.instance.GetUserArmor();
        ArmorData armorData = Resources.Load<ArmorData>("ArmorData/" + selectArmor.key);
        curEuiptmentNameText.text = armorData.armorName;
        curEuiptmentImage.sprite = armorData.sprite;
    }

    public void ArmorSelected(UserArmor userArmor)
    {
        if (userArmor == null)
        {
            curEuiptmentNameText.enabled = false;
            curEuiptmentImage.enabled = false;
            return;
        }

        //UserManager.instance.ChangeArmor(userArmor);
        //Player.Instance.ChangeWeapon(armorData.key);

        ArmorData armorData = Resources.Load<ArmorData>($"WeaponData/{userArmor.key}");

        curEuiptmentNameText.enabled = true;
        curEuiptmentImage.enabled = true;

        curEuiptmentNameText.text = armorData.armorName;
        curEuiptmentImage.sprite = armorData.sprite;
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
