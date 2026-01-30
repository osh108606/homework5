using DG.Tweening;
using TMPro;
using UnityEngine;
public class DamageText : MonoBehaviour
{
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
            damageTextPrefab = Resources.Load<DamageText>("UI/ArmorDamageText");
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

    public void Show(Vector3 pos, string damage)//제거 관련을 비활성화로 바꾸기
    {
        transform.position = pos;
        damageText.text = damage;
        /*
        t += Time.deltaTime;
        if (t > 2f)
        {
            gameObject.SetActive(false);
        }
        Destroy(gameObject,3);
        */
        transform.localScale = Vector3.zero;
        transform.DOScale(1.1f, 0.3f).WaitForCompletion();
        new WaitForSeconds(0.4f);
        transform.DOMoveY(pos.y + 1, 0.7f).WaitForCompletion();
        transform.DOScale(0f, 0.3f).WaitForCompletion();

    }
}
