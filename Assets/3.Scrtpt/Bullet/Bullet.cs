using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed;
    public Weapon weapon;
    public Vector2 direction;
    float t;
    protected DamageInfo damageInfo = new DamageInfo();

    public void Shoot (Vector2 dir, Weapon w)
    {
        t = 0;
        direction = dir;
        weapon = w;
        damageInfo.Init();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        t += Time.deltaTime;
        if (t > 2f)
        {
            gameObject.SetActive(false);
        }

        transform.position = (Vector2)transform.position + (direction * moveSpeed).normalized * Time.deltaTime;
    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //Debug.Log("attack");
            damageInfo.Calculate();
            collision.GetComponent<Enemy>().TakeDamage(damageInfo.damage, damageInfo.isCrt);
            gameObject.SetActive(false);
        }
    }
}

[System.Serializable]
public class DamageInfo
{
    public float damage;
    public float crtChance;
    public float crtDamage;
    public bool isCrt;

    public void Init()
    {
        crtChance = Player.Instance.playerAbility.CrtChance;
        crtDamage = Player.Instance.playerAbility.CrtDamage;
    }


    public void Calculate()
    {
        isCrt = Random.Range(0, 100) == Player.Instance.playerAbility.CrtChance;
        damage = Player.Instance.playerAbility.Damage;
        if (isCrt)
        {
            damage *= Player.Instance.playerAbility.CrtDamage;
        }
    }
}
