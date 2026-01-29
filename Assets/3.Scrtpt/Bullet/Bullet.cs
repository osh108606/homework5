using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float movespeed;
    public Weapon weapon;
    public Vector2 direction;
    float t = 0f;
    protected DamagInfo damagInfo = new DamagInfo();

    public void Shoot (Vector2 dir, Weapon weapon)
    {
        direction = dir;
        this.weapon = weapon;
        damagInfo.Init();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        t += Time.deltaTime;
        if (t > 2f)
        {
            Destroy(this.gameObject);
        }
            transform.position = (Vector2)transform.position + direction* movespeed *Time.deltaTime;
    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //Debug.Log("attack");
            damagInfo.Calculate();
            collision.GetComponent<Enemy>().TakeDamage(damagInfo.damage, damagInfo.isCrt);
            Destroy(this.gameObject);
        }
    }
}

[System.Serializable]
public class DamagInfo
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
