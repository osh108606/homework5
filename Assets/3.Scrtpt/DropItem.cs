using UnityEngine;

public class DropItem : MonoBehaviour
{
    public string key;
    //���Եɲ� ���ǿ� ���� ������Ʈ ������� ����
    //������ ����(key)�� ���� ������ε�(���,�������) �����ɽ� �ش��ϴ� ������key��
    //��ȣ�ۿ� ���
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
