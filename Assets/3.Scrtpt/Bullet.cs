using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject Enemy;
    public float movespeed;
    Vector2 direction;
    float t = 0f;
    public void Shoot (Vector2 dir)
    {
        direction = dir;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t > 2f)
        {
            Destroy(this.gameObject);
        }
            transform.position = (Vector2)transform.position + direction* movespeed *Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
           
            Destroy(this.gameObject);
        }
    }
}
