using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class ArmorSlotPanel : MonoBehaviour
{
    public ArmorEquipSlot armorEquipSlot;
    public ArmorData armorData;
    public Image image;
    public TMP_Text armorName;

    public void Awake()
    {
        image = transform.Find("GPInnerGround").Find("GPImage").GetComponentInChildren<Image>();
        armorName = GetComponentInChildren<TMP_Text>();
    }

    public void SetUserArmor(UserArmor userArmor)
    {
        if (userArmor == null)
        {
            image.enabled = false;
            armorName.text = "none";
        }
        else
        {
            armorData = Resources.Load<ArmorData>("ArmorData/" + userArmor.key);
            image.enabled = true;
            armorName.text = armorData.armorName;
            armorEquipSlot = armorData.armorEquipSlot;

        }
    }

    public void OnClick()
    {
        InventoryCanvas.Instance.OpenArmorInventory(armorEquipSlot);
    }
}
