using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class NPC : MonoBehaviour
{
    public float maxHealthPoint; //최대 체력
    public float maxArmorPoint; //최대 방어도
    public float healthPoint; //현재 체력
    public float armorPoint; //현재 방어도
    public float moveSpeed; //이동 속도

    public Image hpBar;
    public bool ignoreDamage;
    public bool arrived;
    public float y;
    public Vector2 desPoint;

    public virtual void Awake()
    {
        arrived = false;
    }

    public virtual void Start()
    {
        healthPoint = maxHealthPoint;
        armorPoint = maxArmorPoint;
    }
   
    public virtual void Update()
    {
        if(ignoreDamage != false)
            hpBar.fillAmount = healthPoint / maxHealthPoint;

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
        if (arrived)
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
        #region 오브젝트풀링선행 변수&로직
        bool allActive = true;
        List <DamageText> dTextPool = new List <DamageText>();
        DamageText dText;
        //활성상태 체크
        for (int i = 0; i < dTextPool.Count; i++)
        {
            if (dTextPool[i].gameObject.activeSelf == false)
            {
                allActive = false;
                break;
            }
        }
        #endregion
        
        if (dTextPool.Count <= 0 || allActive == true)  //풀링조건문
        {
            if (ignoreDamage == false)  //무적여부조건문
            {
                if (armorPoint > 0) //방어도 데미지 조건문
                {
                    armorPoint -= damage;
                    damage = -armorPoint;
                    dText = DamageText.Instantiate(true, crt);
                    dTextPool.Add(dText);
                    dText.Show(transform.position + new Vector3(0, 2) + (Vector3)Random.insideUnitCircle,
                        ((int)damage).ToString("D0"));
                }
                else if (armorPoint <= damage)
                {
                    armorPoint = 0;
                    damage -= armorPoint;
                    healthPoint -= damage;
                    dText = DamageText.Instantiate(false, crt);
                    dText.Show(transform.position + new Vector3(0, 2), ((int)damage).ToString("D0"));
                }
                
                
                if (healthPoint <= 0)
                {
                    Death();
                    Destroy(this.gameObject);
                }
            }
        }
        else  //풀링조건문
        {
            if (ignoreDamage == false)  //무적여부조건문
            {
                if (armorPoint > 0) //방어도 데미지 조건문
                {
                    for (int i = 0; i < dTextPool.Count; i++)
                    {
                        if (dTextPool[i].gameObject.activeSelf == false)
                        {
                            dTextPool[i].gameObject.SetActive(true);
                            dTextPool[i].Show(transform.position + new Vector3(0, 2) + (Vector3)Random.insideUnitCircle,
                                ((int)damage).ToString("D0"));
                            break;
                        }
                    }
                }
                else if (armorPoint <= damage)
                {
                    armorPoint = 0;
                    damage -= armorPoint;
                    healthPoint -= damage;
                    dText = DamageText.Instantiate(false, crt);
                    dText.Show(transform.position + new Vector3(0, 2), ((int)damage).ToString("D0"));
                }

                if (healthPoint <= 0)
                {
                    Death();
                    Destroy(this.gameObject);
                }
            }
        }
        
    }
    //NPC가 죽을때 발동
    public virtual void Death()
    {
 
    }
}

