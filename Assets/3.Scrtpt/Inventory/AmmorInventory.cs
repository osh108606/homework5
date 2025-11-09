using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmorInventory : SubInventory
{
    public GameObject ammorPanelPrefab;
    public GridLayoutGroup ammorList;
    public List<GameObject> ammorPanels;
    public AmmorEquipSlot ammorEquipSlot;

    public override void OnEnable()
    {
        ammorList = GetComponentInChildren<GridLayoutGroup>();
        curEuiptmentImage = GetComponentInChildren<Image>();
        curEuiptmentNameText = GetComponentInChildren<TMP_Text>();
    }

    public void Open(AmmorEquipSlot EquipSlot)
    {
        ammorEquipSlot = EquipSlot;
        gameObject.SetActive(true);

        UpdateCanvas();
        for (int i = 0; i < ammorPanels.Count; i++)
        {
            Destroy(ammorPanels[i]);
        }
        ammorPanels.Clear();

        for (int i = 0; i < UserManager.instance.userData.userAmmors.Count; i++)
        {
            string key = UserManager.instance.userData.userAmmors[i].key;
            AmmorData ammorData = Resources.Load<AmmorData>("AmmorData/" + key);
            if (ammorData.ammorEquipSlot == ammorEquipSlot)
            {
                GameObject panel = Instantiate(ammorPanelPrefab, ammorList.transform);
                UserAmmor userAmmor = UserManager.instance.userData.userAmmors[i];
                panel.GetComponent<AmmorPanel>().SetData(userAmmor);
                ammorPanels.Add(panel);
            }
        }
        AmmorSelected(null);
    }

    public override void UpdateCanvas()
    {
        UserAmmor selectAmmor = UserManager.instance.GetUserAmmor();
        AmmorData ammorData = Resources.Load<AmmorData>("AmmorData/" + selectAmmor.key);
        curEuiptmentNameText.text = ammorData.ammorName;
        curEuiptmentImage.sprite = ammorData.sprite;
    }

    public void AmmorSelected(AmmorData ammorData)
    {
        if (ammorData == null)
        {
            curEuiptmentNameText.enabled = false;
            curEuiptmentImage.enabled = false;
            return;
        }

        UserManager.instance.ChangeWeapon(ammorData.key);
        Player.instance.ChangeWeapon(ammorData.key);



        curEuiptmentNameText.enabled = true;
        curEuiptmentImage.enabled = true;

        curEuiptmentNameText.text = ammorData.ammorName;
        curEuiptmentImage.sprite = ammorData.sprite;
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
