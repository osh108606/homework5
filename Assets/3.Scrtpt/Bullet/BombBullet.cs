using UnityEngine;
//using static UnityEngine.RuleTile.TilingRuleOutput;

public class BombBullet : Bullet
{
    public Transform tr;
    public float sDamage;
    public bool colCheck;
    

    // Update is called once per frame
    public override void Update()
    {
        if(colCheck == true)
        {
            return;        
        }
        base.Update();
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            colCheck = true;
            tr.localScale= Vector3.one * 2;
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 4);
            for(int i = 0; i <cols.Length; i++)
            {
                if(cols[i].CompareTag("Enemy"))
                {
                    damageInfo.Calculate();
                    //cols[i].GetComponent<Enemy>().TakeDamage(sDamage);
                }
            }
            
            Destroy(this.gameObject,0.1f);
        }
    }
}
