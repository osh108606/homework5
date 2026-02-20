using UnityEngine;

public class Enemy : NPC
{
    public EnemyType enemyType;
    public GameObject dropItemPrefab;
    public EnemyState enemyState;
    public Rigidbody2D rg2d;
    public Transform attackPointTr;//공격 시작포인트
    public EnemyInfo enemyInfo; //적정보
    public Transform rootTr;
    public float attackDelay;  //공격행위 주기
    public bool attacking;
    public override void Awake()
    {
        base.Awake();
        rootTr = transform.Find("Root");
        enemyInfo = Resources.Load<EnemyInfo>($"Enemy/{enemyType}");
        maxHealthPoint = enemyInfo.maxHp;
        maxArmorPoint = enemyInfo.maxAp;
        healthPoint = maxHealthPoint;
        armorPoint = maxArmorPoint;                  
        attackDelay = enemyInfo.attackDelay;
        moveSpeed = enemyInfo.moveSpeed;
    }

    public override void Start()
    {
        base.Start();
        SetState(EnemyState.Idle);
        rg2d = GetComponent<Rigidbody2D>();
    }
    
    public override void Update()
    {
        base.Update();
        attackDelay += Time.deltaTime;
        if (enemyState == EnemyState.Idle)
        {
            //어떤 계산이 필요한가요?
            //시야 범위 안에 플레이어가 있는지 판별!
            //조건이 참이면 상태를 Approaching으로 바꾸기
            IdleState();

        }
        else if (enemyState == EnemyState.Approaching)
        {
            ApproachingState();
        }
        else if (enemyState == EnemyState.Attack)
        {
            if(attacking == false)
                AttackState();
            //총알 발사하기! + 어떤 고려사항이 필요한가요?
            //float attackTimer 
        }
    }
    //상태변경
    public virtual void SetState(EnemyState eState)
    {
        enemyState = eState;
    }

    public virtual void IdleState()
    {
        float distance = Vector2.Distance(transform.position, Player.Instance.transform.position);
        if (distance >= enemyInfo.attackRange && distance < enemyInfo.sightRange)// 조건 좀더 생각해볼것
        {
            SetState(EnemyState.Approaching);
            return;
        }
        else if (distance <= enemyInfo.attackRange && enemyInfo.attackSpeed <= attackDelay)
        {
            SetState(EnemyState.Attack);
        }
        rg2d.linearVelocity = Vector2.zero; 
    }

    public virtual void ApproachingState()
    {
        // 벡터 (방향 * 크기)= 목적지 - 출발지
        Vector2 dir = (Player.Instance.upperTransform.position - transform.position).normalized;
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
        float distance = Vector2.Distance(transform.position, Player.Instance.upperTransform.position);
        if (distance > enemyInfo.sightRange)
        {
            SetState(EnemyState.Idle);
            return;
        }

        if (distance <= enemyInfo.attackRange)
        {
            SetState(EnemyState.Attack);
        }
        
        rg2d.linearVelocity = dir * moveSpeed;

    }
    public virtual void AttackState()
    {
        //Debug.Log("Enemy Attack");

        
        if (enemyInfo.attackSpeed <= attackDelay) //공격할 수 있음!
        {
            Attack();
            rg2d.linearVelocity = Vector2.zero;
            attackDelay = 0;
        }
        else
        {
            SetState(EnemyState.Idle);
        }
    }

    public virtual void Attack()
    {
        attacking = false;
        SetState((EnemyState.Idle));
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
    public override void TakeDamage(float damage ,bool crt)
    {
        base.TakeDamage(damage ,crt);
        Debug.Log("attack");
        if (healthPoint <= 0)
        {
            Death();
            Destroy(this.gameObject);
        }
    }
    
    public override void Death()
    {
        GameObject drop = Instantiate(dropItemPrefab);
        drop.GetComponent<DropItem>().Drop("Weapon2");
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
    Approaching,
    Attack,
}

