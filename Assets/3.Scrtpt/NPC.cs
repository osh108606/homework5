using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public float mhp; //최대 체력
    public float hp; // 현재 체력
    public float moveSpeed; // 이동 속도

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
    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Death();
            Destroy(this.gameObject);
        }
    }
    //NPC가 죽을때 발동
    public void Death()
    {
 
    }
}

