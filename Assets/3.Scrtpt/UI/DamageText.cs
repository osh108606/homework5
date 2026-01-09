using UnityEngine;
using TMPro;
public class DamageText : MonoBehaviour
{
    public static DamageText Instantiate()
    {
        DamageText damageTextPrefab = Resources.Load<DamageText>("UI/DamageText");
        DamageText damageText = Instantiate(damageTextPrefab);
        return damageText;
    }

    public TMP_Text damageText;

    public void Show(Vector3 pos, string damage)
    {
        transform.position = pos;
        damageText.text = damage;

        Destroy(gameObject,1);
    }
}
