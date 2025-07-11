using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int mhp;
    public Scrollbar scrollbar;
    public Canvas canvas;
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
        
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            Debug.Log(hp);
            hp--;
            scrollbar.size--;
        }
    }
}
