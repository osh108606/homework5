using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpanwer : MonoBehaviour, IEnemySpawner
{
    public Enemy enemyPrefab; //��ȯ ���
    public int count;// ��
    public float interval;// ����
    public int currentCount;// �����
    

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
            enemy.transform.parent = transform;
        }
    }

}
