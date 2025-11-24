using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpanwer : MonoBehaviour, IEnemySpawner
{
    public Enemy enemyPrefab; //소환 대상
    public int count;// 수
    public float interval;// 간격
    public int currentCount;// 현재수
    

    public  virtual void StartSpawn()
    {
        currentCount = count;
        StartCoroutine(CoSpawn());
    }


    public void KilledEnemy(Enemy e)
    {
        currentCount--;
        if (currentCount == 0)
        {
            GetComponentInParent<DungeonWave>().SquadDie(this);
        }
    }

    public Enemy enemy;
    IEnumerator CoSpawn()
    {
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(interval);
            enemy = Instantiate(enemyPrefab, this.transform.position, Quaternion.identity);
            enemy.transform.parent = transform;
        }
    }

}
