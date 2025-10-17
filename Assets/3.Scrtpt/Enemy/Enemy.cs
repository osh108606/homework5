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
    public GameObject dropItemPrifap;
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
            Death();
            Destroy(this.gameObject);
        }
    }
    
    public void Death()
    {
        GameObject drop = Instantiate(dropItemPrifap);
        drop.GetComponent<DropItem>().SetItemKey("Weapon2");
        drop.transform.position = transform.position;
        IEnemySpawner zone = GetComponentInParent<IEnemySpawner>();
        if (zone != null)
        {
            zone.KilledEnemy(this);
        }
    }

    public void Spawn(Vector2 startPos, Vector2 initArrPos)
    {
        transform.position = startPos;
    }
}

