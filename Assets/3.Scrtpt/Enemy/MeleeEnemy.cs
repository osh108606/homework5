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
                HitBox hitBox = cols[i].GetComponent<HitBox>();
                if (hitBox != null)
                {
                    Debug.Log("Attack()");
                    Player.Instance.TakeDamage(enemyInfo.attackDamage);
                    return;
                }
            }
        }
    }

    public override void AttackState()
    {
        if (enemyInfo.attackSpeed <= attackDelay) //공격할 수 있음!
        {
            attackOutline.gameObject.SetActive(true);
            attacking = true;
            rg2d.linearVelocity = Vector2.zero;
            attackIndicator.localScale = Vector3.zero;
            attackIndicator.DOScale(Vector3.one,enemyInfo.attackSpeed).OnComplete((() =>
            {
                Attack();
                attackOutline.gameObject.SetActive(false);
            }));
            attackDelay = 0;
        }
        else
        {
            SetState(EnemyState.Idle);
        }
        
    }
}
