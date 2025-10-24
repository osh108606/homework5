using UnityEditor.SceneManagement;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject Enemy;
    public float movespeed;
    public Weapon weapon;
    public Vector2 direction;
    float t = 0f;
    public void Shoot (Vector2 dir, Weapon weapon)
    {
        direction = dir;
        this.weapon = weapon;
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
            collision.GetComponent<Enemy>().TakeDamage(weapon.weaponData.damage);
            Destroy(this.gameObject);
        }
    }
}
