using UnityEngine;

public class RangedEnemy : Enemy
{
    public EnemyBullet bulletPrefab;//����� �Ѿ�
    

    public override void Attack()
    {
        base.Attack();
        //Vector2 directtion = Player.instance.Bodytr.transform.position - attackPointTr.transform.position;
        Vector2 directtion = Player.instance.transform.position - attackPointTr.transform.position;
        EnemyBullet bullet = Instantiate(bulletPrefab);
        bullet.gameObject.transform.position = attackPointTr.transform.position;
        bullet.Shoot(directtion.normalized);
    }
}
