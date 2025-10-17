using UnityEngine;
using System.Collections;

public class DungeonWave : MonoBehaviour
{
    public EnemySpanwer[] enemySpanwers;
    public int squedCount;
    public bool waveEnd;
    public void Awake()
    {
        waveEnd = false;
        enemySpanwers = GetComponentsInChildren<EnemySpanwer>();
    }
    public void StartWave()
    {
        waveEnd = false;
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
