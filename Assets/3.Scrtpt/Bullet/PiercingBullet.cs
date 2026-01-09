using UnityEngine;

public class PiercingBullet : Bullet
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            damagInfo.Calculate();
            collision.GetComponent<Enemy>().TakeDamage(damagInfo.damage, damagInfo.isCrt);
        }
    }
}
