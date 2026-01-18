using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GearPanel : MonoBehaviour
{
    public Image image;    
    public TMP_Text text;
    public bool select = false;
    public virtual void Awake()
    {
        image = GetComponentInChildren<Image>();
        text = GetComponentInChildren<TMP_Text>();
    }
    public virtual void SetData(UserWeapon userWeapon)
    {
        
    }
    public virtual void SetData(UserArmor userArmor)
    {

    }
    public virtual void OnClicked()
    {
        
    }
   
}
