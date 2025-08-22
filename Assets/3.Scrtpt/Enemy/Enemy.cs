using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float mhp;
    public Image HpBar;
    float hp;
    float z;
    float y;
    public float movespeed;
    public Vector2 desPoint;
    void Start()
    {
        
        hp = mhp;
    }
    public bool arrived = false;
    // Update is called once per frame
    void Update()
    {
        HpBar.fillAmount = hp/mhp;
        

        
        
        float distance = Vector2.Distance(transform.position, desPoint);
        if(arrived == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, desPoint, 30 * Time.deltaTime);
        }
        
        if (distance <= 0.1f)
        {
            arrived = true;
        }
        
    }

    public void Move()
    {
        y = transform.position.y;
        if (arrived == true)
        {
            y -= Time.deltaTime * movespeed;
            transform.position = new Vector2(transform.position.x, y);
        }
    }
    public void TakeDamage(float damage)
    {
        hp-= damage;
        if (hp <= 0)
        {
            EnemyController.instance.enemiesCount--;
            EnemyController.instance.cheak--;
            Destroy(this.gameObject);
        }
    }
    
}
