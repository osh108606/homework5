using UnityEngine;

public class DungeonWave : MonoBehaviour
{
    public EnemySpawner[] enemySpawners;
    public int squadCount;
    public bool waveEnd;
    public void Awake()
    {
        enemySpawners = GetComponentsInChildren<EnemySpawner>();
    }

    public void Init()
    {
        waveEnd = false;
        squadCount = 0;
        for (int i =0; i< enemySpawners.Length; i++)
        {
            for(int j =0; j< enemySpawners[i].enemies.Count; j++)
            {
                if(enemySpawners[i].enemies[j] != null)
                    enemySpawners[i].enemies[j].EnemyDelete();
            }
            enemySpawners[i].enemies.Clear();
        }
    }

    public void StartWave()
    {
        Debug.Log("StartWave");
        squadCount = enemySpawners.Length;
        for (int i = 0; i < enemySpawners.Length; i++)
        {
            enemySpawners[i].StartSpawn();
        }
    }

    public void SquadDie(EnemySpawner enemySpawner)
    {
        squadCount--;
        if(squadCount <= 0)
        {
            waveEnd = true;
            GetComponentInParent<DungeonZone>().WaveEnd();
        }
       
    }
}
