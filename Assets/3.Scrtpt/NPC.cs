using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public float mhp; //�ִ� ü��
    public float hp; // ���� ü��
    public float moveSpeed; // �̵� �ӵ�

    public Collider2D col;
    public GameObject canvasObjede;
    public Image HpBar;
    public StageSelect stageSelect;
    bool arrived = false;
    float y;
    public Vector2 desPoint;
    
    void Start()
    {
        hp = mhp;
    }
   
    void Update()
    {
        HpBar.fillAmount = hp / mhp;

        //float distance = Vector2.Distance(transform.position, desPoint);
        //if (arrived == false)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, desPoint, 30 * Time.deltaTime);
        //}

        //if (distance <= 0.1f)
        //{
        //    arrived = true;
        //}

    }

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

    public void Move()
    {
        y = transform.position.y;
        if (arrived == true)
        {
            y -= Time.deltaTime * moveSpeed;
            transform.position = new Vector2(transform.position.x, y);
        }
    }

    public void TalkUI()
    {
        if (stageSelect.gameObject.activeSelf == true)
        {
            stageSelect.gameObject.SetActive(false);
        }
        else
        {
            stageSelect.gameObject.SetActive(true);
        }
    }

    //�����̵�
    public void RandomMove()
    {

    }
    //Ư�� �����̵�
    public void PointMove()
    {

    }
    // Ư�� ����̵�
    public void TargetMove()
    {

    }

    //NPC�� �������� ������ �ߵ�
    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Death();
            Destroy(this.gameObject);
        }
    }
    //NPC�� ������ �ߵ�
    public void Death()
    {
 
    }
}

