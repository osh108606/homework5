using UnityEngine;
using DG.Tweening;
public class MeleeEnemy : Enemy
{
    public Transform attackOutline;
    public Transform attackIndicator;

    public override void Attack()
    {
        base.Attack();
        Collider2D[] cols = Physics2D.OverlapCircleAll(attackPointTr.position, enemyInfo.attackRange);

        if (cols.Length <= 0)
            return;

        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].CompareTag("Player"))
            {
                Player player = cols[i].GetComponent<Player>();
                //if(player == null)
                    //Debug.Log("X");
                player.TakeDamage(enemyInfo.attackDamage);
            }
        }
    }

    public override void SetState(EnemyState eState)
    {
        base.SetState(eState);
        if (eState == EnemyState.Attack)
        {
            attackOutline.gameObject.SetActive(true);
        }
    }

    public override void AttackState()
    {
        base.AttackState();
        attackIndicator.localScale = Vector3.zero;
        attackIndicator.DOScale(Vector3.one,enemyInfo.attackSpeed).OnComplete((() =>
        {
            Attack();
            attackOutline.gameObject.SetActive(false);
            attackDelay = 0;
        }));
    }
}
