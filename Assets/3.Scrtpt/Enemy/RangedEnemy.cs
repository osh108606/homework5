using UnityEngine;

public class RangedEnemy : Enemy
{
    public EnemyBullet bulletPrefab;//»ç¿ëÇÒ ÃÑ¾Ë
    

    public override void Attack()
    {
        base.Attack();
        //Vector2 direction = Player.Instance.BodyTr.transform.position - attackPointTr.transform.position;
        Vector2 direction = Player.Instance.upperTransform.transform.position - attackPointTr.transform.position;
        EnemyBullet bullet = Instantiate(bulletPrefab);
        bullet.enemyInfo = this.enemyInfo;
        bullet.gameObject.transform.position = attackPointTr.transform.position;
        bullet.Shoot(direction.normalized);
    }
}
