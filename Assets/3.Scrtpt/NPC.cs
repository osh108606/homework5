using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public float mhp; //�ִ� ü��
    public float hp; // ���� ü��
    public float moveSpeed; // �̵� �ӵ�

    
    public Image HpBar;
    
    public bool arrived = false;
    public float y;
    public Vector2 desPoint;

    public virtual void Awake()
    {

    }

    public virtual void Start()
    {
        hp = mhp;
    }
   
    public virtual void Update()
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

    

    public virtual void Move()
    {
        y = transform.position.y;
        if (arrived == true)
        {
            y -= Time.deltaTime * moveSpeed;
            transform.position = new Vector2(transform.position.x, y);
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
    public virtual void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Death();
            Destroy(this.gameObject);
        }
    }
    //NPC�� ������ �ߵ�
    public virtual void Death()
    {
 
    }
}

