using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, IEnemySpawner
{
    public Enemy enemyPrefab; //소환 대상
    public int count;// 수
    public float interval;// 간격
    public int currentCount;// 현재수
    public List<Enemy>enemies = new List<Enemy>();

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

    IEnumerator CoSpawn()
    {
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(interval);
            Enemy enemy = Instantiate(enemyPrefab, this.transform.position, Quaternion.identity);
            enemies.Add(enemy);
            enemy.transform.parent = transform;
        }
    }

}
