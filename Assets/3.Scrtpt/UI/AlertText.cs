using DG.Tweening;
using UnityEngine;
using TMPro;

public class AlertText : MonoBehaviour
{
    //오브젝트 풀링 - DamageText도
    public static AlertText Instantiate()
    {
        AlertText alertTextPrefab;
        AlertText alertText;
        
            alertTextPrefab = Resources.Load<AlertText>("UI/AlertText");
            alertText = Instantiate(alertTextPrefab);
            return alertText;
    }
    public TMP_Text alertText;

    public void Show(Vector3 pos, string text)
    {
        transform.position = pos;
        alertText.text = text;

        Destroy(gameObject,3);

        transform.localScale = Vector3.zero;
        transform.DOScale(1.1f, 0.3f).WaitForCompletion();
        new WaitForSeconds(0.4f);
        transform.DOMoveY(pos.y + 1, 0.7f).WaitForCompletion();
        transform.DOScale(0f, 0.3f).WaitForCompletion();

    }
}