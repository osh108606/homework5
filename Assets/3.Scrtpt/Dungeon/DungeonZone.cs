using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class DungeonZone : MonoBehaviour, IEnemySpawner
{
    public string key;
    public int order;
    public Transform playerRespawnPoint;
    //던전 구역 플레이어 리스폰포인트
    public Transform[] enemySpawnPoints;
    //던전구역 시작 적 스폰포인트
    public DungeonWave[] dungeonWaves;
    //던전구역 웨이브들
    public Dungeon dungeon;

    List<Enemy> enemies = new List<Enemy>();
    public Enemy enemyPrefab;
    [FormerlySerializedAs("firstEnemey")] [FormerlySerializedAs("firstEnmey")] public int firstEnemy;
    public int waveCount;
    public bool zoneEnd;

    public void Awake()
    {
        dungeon = GetComponentInParent<Dungeon>();
        zoneEnd = false;
        firstEnemy = 0;
        dungeonWaves = GetComponentsInChildren<DungeonWave>();
        //enemySpawnPoints = GetComponentsInChildren<Transform>();
    }
    
    public void ZoneStart()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if( enemies[i] != null )
                enemies[i].EnemyDelete();
        }
        enemies.Clear();
        
        for (int i = 0; i < dungeonWaves.Length; i++)
        {
            dungeonWaves[i].Init();
        }


        dungeon.curZone = this;
        Debug.Log("DungeonZone Start");
        firstEnemy = 0;
        waveCount = dungeonWaves.Length;
        for (int i = 0; i < enemySpawnPoints.Length; i++)
        {
            Enemy enemy = Instantiate(enemyPrefab, enemySpawnPoints[i].transform.position, Quaternion.identity);
            enemies.Add(enemy);
            enemy.transform.parent = enemySpawnPoints[i].transform;
            firstEnemy++;
        }

    }

    public void WaveEnd()
    {
        Debug.Log("WaveEnd");
        waveCount--;
        if (waveCount == 0)
        {
            zoneEnd = true;
            GetComponentInParent<Dungeon>().ZoneEnd();
        }
        for(int i = 1; i< dungeonWaves.Length; i++ )
        {
            if( dungeonWaves[i-1].waveEnd && dungeonWaves[i].waveEnd == false)
            {
                dungeonWaves[i].StartWave();
                break;
            }
        }
    }

    public void KilledEnemy(Enemy e)
    {
        firstEnemy--;
        Debug.Log(firstEnemy);
        if (firstEnemy == 0)
        {
            Debug.Log("FirstEnemy End");
            dungeonWaves[0].StartWave();
        }
    }

    //1. 첫웨이브에서 두번째 가는 기준설정
    //2. 마지막 웨이브 클리어 했는지 체크 - 던전존 클리어 로그 출력
    //3. 플레이가 될 때 까지 구현
}
