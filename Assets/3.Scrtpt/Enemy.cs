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
        y=transform.position.y;
    }
    bool a = false;
    // Update is called once per frame
    void Update()
    {
        HpBar.fillAmount = hp/mhp;
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }

        
        
        float distance = Vector2.Distance(transform.position, desPoint);
        if(a == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, desPoint, 30 * Time.deltaTime);
        }
        
        if (distance <= 0.1f)
        {
            a = true;
            y -= Time.deltaTime * movespeed;
            transform.position = new Vector2(transform.position.x, y);
        }
        
        
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            hp--;
            
        }
    }
}
