using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubInventory : MonoBehaviour
{
    public TMP_Text curEuiptmentNameText;
    public Image curEuiptmentImage;
    public virtual void Awake()
    {
        curEuiptmentNameText = GetComponentInChildren<TMP_Text>();
        curEuiptmentImage = GetComponentInChildren<Image>();
    }
    public virtual void UpdateCanvas()
    {

        
    }
}
