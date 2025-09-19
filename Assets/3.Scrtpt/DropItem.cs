using UnityEngine;

public class DropItem : MonoBehaviour
{
    public string key;
    //포함될꺼 조건에 따라 오브젝트 색상등이 변경
    //아이템 정보(key)값 소지 어떤이유로든(드랍,버리기등) 생성될시 해당하는 아이템key값
    //상호작용 방법
    public Collider2D col;
    public GameObject canvasObjede;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canvasObjede.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canvasObjede.SetActive(false);
        }      
    }
}
