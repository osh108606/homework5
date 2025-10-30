using UnityEngine;
using UnityEngine.UI;

public class Enemy : NPC
{
    public GameObject dropItemPrifap;
    public EnemyState enemyState;
    public Rigidbody2D rg2d;
    public Transform attackPointTr;//���� ��������Ʈ
    public EnemyInfo enemyInfo; //������

    public float sightRange; //��������
    public float attackRange; //���ݹ���
    public float attackSpeed; //���� �ӵ� ex�Ѿ��� �󸶳� ������°� (���� ������ x)
    public float attackDelay;  //�������� �ֱ�
    public float attackDamage; //������

    public override void Awake()
    {
        base.Awake();
        mhp = enemyInfo.Maxhp;
        attackRange = enemyInfo.attackRange;
        moveSpeed = enemyInfo.moveSpeed;
        attackDelay = enemyInfo.attackDelay;
        attackDamage = enemyInfo.attackDamage;

    }

    public override void Start()
    {
        base.Start();
        SetState(EnemyState.Idle);
        rg2d = GetComponent<Rigidbody2D>();
        //EnemyController.instance.enemiesCount++;
        //EnemyController.instance.cheak++;
    }

    

    
    public override void Update()
    {
        base.Update();
        attackDelay += Time.deltaTime;
        if (enemyState == EnemyState.Idle)
        {
            //� ����� �ʿ��Ѱ���?
            //�þ� ���� �ȿ� �÷��̾ �ִ��� �Ǻ�!
            //������ ���̸� ���¸� Approching���� �ٲٱ�
            IdleState();


        }
        else if (enemyState == EnemyState.Approching)
        {
            ApprochingState();
        }
        else if (enemyState == EnemyState.Attack)
        {
            AttackState();
            //�Ѿ� �߻��ϱ�! + � ��������� �ʿ��Ѱ���?
            //float attackTimer 
        }
    }
    //���º���
    public void SetState(EnemyState eState)
    {
        enemyState = eState;
    }

    public virtual void IdleState()
    {
        float distance = Vector2.Distance(transform.position, Player.instance.transform.position);
        if (distance >= attackRange&& distance < sightRange)// ���� ���� �����غ���
        {
            SetState(EnemyState.Approching);
            return;
        }
        rg2d.linearVelocity = Vector2.zero;
    }

    public virtual void ApprochingState()
    {
        // ���� (���� * ũ��)= ������ - �����
        Vector2 dir = (Player.instance.transform.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, Player.instance.transform.position);
        if (distance > sightRange)
        {
            SetState(EnemyState.Approching);
        }

        if (distance <= attackRange)
        {
            SetState(EnemyState.Attack);
        }
        rg2d.linearVelocity = dir * moveSpeed;

    }
    public virtual void AttackState()
    {
        Debug.Log("Enemy Attack");

        rg2d.linearVelocity = Vector2.zero;
        if (attackSpeed <= attackDelay) //������ �� ����!
        {
            Attack();
            attackDelay = 0;
        }
        else
        {
            SetState(EnemyState.Idle);
        }
    }

    public virtual void Attack()
    {

    }


    public override void Move()
    {
        y = transform.position.y;
        if (arrived == true)
        {
            y -= Time.deltaTime * moveSpeed;
            transform.position = new Vector2(transform.position.x, y);
        }
    }
    public override void TakeDamage(float damage)
    {
        hp-= damage;
        if (hp <= 0)
        {
            //EnemyController.instance.enemiesCount--;
            //EnemyController.instance.cheak--;
            Death();
            Destroy(this.gameObject);
        }
    }
    
    public override void Death()
    {
        GameObject drop = Instantiate(dropItemPrifap);
        drop.GetComponent<DropItem>().SetItemKey("Weapon2");
        drop.transform.position = transform.position;
        IEnemySpawner zone = GetComponentInParent<IEnemySpawner>();
        if (zone != null)
        {
            zone.KilledEnemy(this);
        }
    }

    public void Spawn(Vector2 startPos, Vector2 initArrPos)
    {
        transform.position = startPos;
    }
}
public enum EnemyState
{
    Idle,
    Approching,
    Attack,
}

