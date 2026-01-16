using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArmorSlotPanel : MonoBehaviour
{
    public ArmorEquipSlot armorEquipSlot;
    public ArmorData armorData;
    public Image thumImage;
    public TMP_Text armorName;

    public void Awake()
    {
        thumImage = GetComponentInChildren<Image>();
        armorName = GetComponentInChildren<TMP_Text>();
    }

    public void SetUserArmor(UserArmor userArmor)
    {
        if (userArmor == null)
        {
            thumImage.enabled = false;
            armorName.text = "none";
        }
        else
        {
            armorData = Resources.Load<ArmorData>("ArmorData/" + userArmor.key);
            thumImage.enabled = true;
            armorName.text = armorData.armorName;
            armorEquipSlot = armorData.armorEquipSlot;

        }
    }

    public void OnClick()
    {
        InventoryCanvas.Instance.OpenArmorInventory(armorEquipSlot);
    }
}
