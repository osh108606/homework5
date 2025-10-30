using UnityEngine;

public class MeleeEnemy : Enemy
{
    public override void Attack()
    {
        base.Attack();
        Collider2D[] cols = Physics2D.OverlapCircleAll(attackPointTr.position, enemyInfo.attackRange);

        if (cols.Length <= 0)
        {
            return;
        }

        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].CompareTag("Player"))
            {
                Player player = cols[i].GetComponent<Player>();
                player.TakeDamage(enemyInfo.attackDamage);
            }
        }
    }
}
