using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubInventory : MonoBehaviour
{
    public TMP_Text curEuiptmentNameText;
    public Image curEuiptmentImage;

    public virtual void OnEnable()
    {
        curEuiptmentImage = GetComponentInChildren<Image>();
        curEuiptmentNameText = GetComponentInChildren<TMP_Text>();
    }
    public virtual void UpdateCanvas()
    {

        
    }
}
