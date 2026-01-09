using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public float maxHealthPoint; //최대 체력
    public float healthPoint; // 현재 체력
    public float moveSpeed; // 이동 속도

    
    public Image HpBar;
    public bool ignoreDamage;
    public bool arrived = false;
    public float y;
    public Vector2 desPoint;

    public virtual void Awake()
    {

    }

    public virtual void Start()
    {
        healthPoint = maxHealthPoint;
    }
   
    public virtual void Update()
    {
        if(ignoreDamage != false)
            HpBar.fillAmount = healthPoint / maxHealthPoint;

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

    

    //자유이동
    public void RandomMove()
    {

    }
    //특정 지점이동
    public void PointMove()
    {

    }
    // 특정 대상이동
    public void TargetMove()
    {

    }

    //NPC가 데미지를 입을때 발동
    public virtual void TakeDamage(float damage, bool crt)
    {
        if (ignoreDamage == false)
        {
            healthPoint -= damage;
            if (healthPoint <= 0)
            {
                Death();
                Destroy(this.gameObject);
            }

        }
    }
    //NPC가 죽을때 발동
    public virtual void Death()
    {
 
    }
}

