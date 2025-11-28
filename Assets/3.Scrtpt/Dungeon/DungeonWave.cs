using UnityEngine;
using System.Collections;

public class DungeonWave : MonoBehaviour
{
    public EnemySpanwer[] enemySpanwers;
    public int squedCount;
    public bool waveEnd;
    public void Awake()
    {
        enemySpanwers = GetComponentsInChildren<EnemySpanwer>();
    }

    public void Init()
    {
        waveEnd = false;
        squedCount = 0;
        for (int i =0; i< enemySpanwers.Length; i++)
        {
            for(int j =0; j< enemySpanwers[i].enemies.Count; j++)
            {
                if(enemySpanwers[i].enemies[j] != null)
                    enemySpanwers[i].enemies[j].EnemyDelete();
            }
            enemySpanwers[i].enemies.Clear();
        }
    }

    public void StartWave()
    {
        Debug.Log("StartWave");
        squedCount = enemySpanwers.Length;
        for (int i = 0; i < enemySpanwers.Length; i++)
        {
            enemySpanwers[i].StartSpawn();
        }
    }

    public void SquadDie(EnemySpanwer enemySpanwer)
    {
        squedCount--;
        if(squedCount <= 0)
        {
            waveEnd = true;
            GetComponentInParent<DungeonZone>().WaveEnd();
        }
       
    }
}
