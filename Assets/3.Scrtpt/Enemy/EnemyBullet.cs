using UnityEngine;
//적의 총알 코드
public class EnemyBullet : MonoBehaviour
{

    public float moveSpeed; //총알속도
    public EnemyInfo enemyInfo; //적정보
    float bulletLifeTime; //총알 수명
    Vector2 direction; //총알 방향
    public LayerMask hitTargetLayer;

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
        transform.position = ((Vector2)transform.position + direction * moveSpeed) * Time.deltaTime;
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -direction , moveSpeed * Time.deltaTime, hitTargetLayer);
        if(hit.collider != null)
        {
            Player player = hit.collider.GetComponentInParent<Player>();
            HitBox playerHitBox = hit.collider.GetComponent<HitBox>();
            player.TakeDamage(enemyInfo.attackDamage, playerHitBox);
            Destroy(gameObject);
            return;
        }
    }
}
