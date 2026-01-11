using UnityEngine;
using TMPro;
public class DamageText : MonoBehaviour
{
    float t = 0f;
    public void Update()
    {
        t += Time.deltaTime;
        if (t > 1f)
        {
            Destroy(this.gameObject);
        }
    }
    public static DamageText Instantiate(bool APorHP,bool crt)
    {
        DamageText damageTextPrefab;
        DamageText damageText;
        if (crt == true)
        {
            damageTextPrefab = Resources.Load<DamageText>("UI/CrticalDamageText");
            damageText = Instantiate(damageTextPrefab);
            return damageText;
        }
        else if (APorHP == true && crt == false)
        {
            damageTextPrefab = Resources.Load<DamageText>("UI/AmmorDamageText");
            damageText = Instantiate(damageTextPrefab);
            return damageText;
        }
        else if (APorHP == false && crt == false)
        {
            damageTextPrefab = Resources.Load<DamageText>("UI/HealthDamageText");
            damageText = Instantiate(damageTextPrefab);
            return damageText;
        }

            return null;
    }

    public TMP_Text damageText;

    public void Show(Vector3 pos, string damage)
    {
        transform.position = pos;
        damageText.text = damage;

        Destroy(gameObject,1);
    }
}
