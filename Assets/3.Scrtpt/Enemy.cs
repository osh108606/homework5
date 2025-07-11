using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int mhp;
    public Image HpBar;
    int hp;
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
        y -= Time.deltaTime * movespeed;
        transform.position = new Vector2(transform.position.x,y);
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
        HpBar.fillAmount = (float)hp/(float)mhp;
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            hp--;
        }
    }
}
