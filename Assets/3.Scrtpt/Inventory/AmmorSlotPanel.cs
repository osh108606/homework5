using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmorSlotPanel : MonoBehaviour
{
    public AmmorEquipSlot ammorEquipSlot;
    public AmmorData ammorData;
    public Image thumImage;
    public TMP_Text ammorName;

    public void Awake()
    {
        thumImage = GetComponentInChildren<Image>();
        ammorName = GetComponentInChildren<TMP_Text>();
    }

    public void SetUserAmmor(UserAmmor userAmmor)
    {
        if (userAmmor == null)
        {
            thumImage.enabled = false;
            ammorName.text = "none";
        }
        else
        {
            ammorData = Resources.Load<AmmorData>("AmmorData/" + userAmmor.key);
            thumImage.enabled = true;
            ammorName.text = ammorData.ammorName;
            ammorEquipSlot = ammorData.ammorEquipSlot;

        }
    }

    public void OnClick()
    {
        InventoryCanvas.Instance.OpenAmmorInventory(ammorEquipSlot);
    }
}
