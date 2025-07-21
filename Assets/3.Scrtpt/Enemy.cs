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
    void Start()
    {
        hp = mhp;
        y=transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //y -= Time.deltaTime * movespeed;
        //transform.position = new Vector2(transform.position.x,y);
        //if (hp <= 0)
        //{
        //    Destroy(this.gameObject);
        //}

        HpBar.fillAmount = hp/mhp;
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            hp--;
        }
    }
}
