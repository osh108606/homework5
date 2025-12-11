using JetBrains.Annotations;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
//적의 총알 코드
public class EnemyBullet : MonoBehaviour
{

    public float movespeed; //총알속도
    public EnemyInfo enemyInfo; //적정보
    float bulletLifeTime = 0; //총알 수명
    Vector2 direction; //총알 방향


    public void Shoot(Vector2 dir)
    {
        direction = dir;
    }

    // Update is called once per frame
    void Update()
    {
        //총알 삭제시간
        bulletLifeTime += Time.deltaTime;
        if (bulletLifeTime > 1f)
        {
            Destroy(gameObject);
        }
        //방향
        transform.position = (Vector2)transform.position + direction * movespeed * Time.deltaTime;
    }


    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponentInParent<Player>();
            HitBox playerHitBox = collision.GetComponent<HitBox>();
            player.TakeDamage(enemyInfo.attackDamage, playerHitBox.hitbox);
            Destroy(gameObject);
        }
    }
}
