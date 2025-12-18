using UnityEngine;
using UnityEngine.UI;

public class Enemy : NPC
{
    public EnemyType enemyType;
    public GameObject dropItemPrifap;
    public EnemyState enemyState;
    public Rigidbody2D rg2d;
    public Transform attackPointTr;//공격 시작포인트
    public EnemyInfo enemyInfo; //적정보
    public Transform rootTr;
    public float attackDelay;  //공격행위 주기

    public override void Awake()
    {
        base.Awake();
        rootTr = transform.Find("Root");
        enemyInfo = Resources.Load<EnemyInfo>($"Enemy/{enemyType}");
        maxHealthPoint = enemyInfo.Maxhp;
        attackDelay = enemyInfo.attackDelay;
        moveSpeed = enemyInfo.moveSpeed;
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
            //어떤 계산이 필요한가요?
            //시야 범위 안에 플레이어가 있는지 판별!
            //조건이 참이면 상태를 Approching으로 바꾸기
            IdleState();


        }
        else if (enemyState == EnemyState.Approching)
        {
            ApprochingState();
        }
        else if (enemyState == EnemyState.Attack)
        {
            AttackState();
            //총알 발사하기! + 어떤 고려사항이 필요한가요?
            //float attackTimer 
        }
    }
    //상태변경
    public void SetState(EnemyState eState)
    {
        enemyState = eState;
    }

    public virtual void IdleState()
    {
        float distance = Vector2.Distance(transform.position, Player.instance.transform.position);
        if (distance >= enemyInfo.attackRange && distance < enemyInfo.sightRange)// 조건 좀더 생각해볼것
        {
            SetState(EnemyState.Approching);
            return;
        }
        else if (distance <= enemyInfo.attackRange && enemyInfo.attackSpeed <= attackDelay)
        {
            SetState(EnemyState.Attack);
        }
            rg2d.linearVelocity = Vector2.zero;
    }

    public virtual void ApprochingState()
    {
        // 벡터 (방향 * 크기)= 목적지 - 출발지
        Vector2 dir = (Player.instance.transform.position - transform.position).normalized;
        if (dir.x > 0)
        {
            //오른쪽
            rootTr.localScale = new Vector2(-1, 1);
        }
        else
        {
            //왼쪽
            rootTr.localScale = new Vector2(1, 1);
        }
        float distance = Vector2.Distance(transform.position, Player.instance.transform.position);
        if (distance > enemyInfo.sightRange)
        {
            SetState(EnemyState.Approching);
        }

        if (distance <= enemyInfo.attackRange)
        {
            SetState(EnemyState.Attack);
        }
        rg2d.linearVelocity = dir * moveSpeed;

    }
    public virtual void AttackState()
    {
        Debug.Log("Enemy Attack");

        rg2d.linearVelocity = Vector2.zero;
        if (enemyInfo.attackSpeed <= attackDelay) //공격할 수 있음!
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
        healthPoint -= damage;
        if (healthPoint <= 0)
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
    public void EnemyDelete()
    {
        Destroy(this.gameObject);
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

