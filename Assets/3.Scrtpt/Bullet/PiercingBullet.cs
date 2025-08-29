using UnityEngine;

public class PiercingBullet : Bullet
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(weapon.weaponData.damage);
        }
    }
}
