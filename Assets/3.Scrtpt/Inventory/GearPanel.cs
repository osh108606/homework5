using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GearPanel : MonoBehaviour
{
    public Image image;
    public TMP_Text text;
    // 유저매니저로 보유한 무기정보의 무기데이터구분자를 받고 그것을 반영하여 내보낸다
    public void SetData(string key)
    {
        text.text = Resources.Load<WeaponData>("WeaponData/" + key).weaponName;
        image.sprite = Resources.Load<WeaponData>("WeaponData/" + key).sprite;
    }
}
