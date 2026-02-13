using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubInventory : MonoBehaviour
{
    public TMP_Text curEquipmentNameText;
    public Image curEquipmentImage;

    public virtual void OnEnable()
    {
        curEquipmentImage = GetComponentInChildren<Image>();
        curEquipmentNameText = GetComponentInChildren<TMP_Text>();
    }
    public virtual void UpdateCanvas()
    {

        
    }
}
