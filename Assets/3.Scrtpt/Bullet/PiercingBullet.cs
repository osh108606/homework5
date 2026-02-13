using UnityEngine;

public class PiercingBullet : Bullet
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            damageInfo.Calculate();
            collision.GetComponent<Enemy>().TakeDamage(damageInfo.damage, damageInfo.isCrt);
        }
    }
}
