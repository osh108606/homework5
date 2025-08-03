using UnityEditor.SceneManagement;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject Enemy;
    public float movespeed;
    public float damge;
    public Vector2 direction;
    float t = 0f;
    public void Shoot (Vector2 dir)
    {
        direction = dir;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        t += Time.deltaTime;
        if (t > 2f)
        {
            Destroy(this.gameObject);
        }
            transform.position = (Vector2)transform.position + direction* movespeed *Time.deltaTime;
    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(damge);
            Destroy(this.gameObject);
        }
    }
}
