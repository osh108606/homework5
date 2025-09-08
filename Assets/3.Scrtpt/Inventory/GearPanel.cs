using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GearPanel : MonoBehaviour
{
    public Image image;
    public TMP_Text text;
    // �����Ŵ����� ������ ���������� ���ⵥ���ͱ����ڸ� �ް� �װ��� �ݿ��Ͽ� ��������
    public void SetData(string key)
    {
        text.text = Resources.Load<WeaponData>("WeaponData/" + key).weaponName;
        image.sprite = Resources.Load<WeaponData>("WeaponData/" + key).sprite;
    }
}
