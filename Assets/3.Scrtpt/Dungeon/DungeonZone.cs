using UnityEngine;
using UnityEngine.UIElements;

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


    public Enemy enemyPrefab;
    public int firstEnmey;
    public int waveCount;
    public bool zoneEnd;

    public void Awake()
    {
        dungeon = GetComponentInParent<Dungeon>(); ;
        zoneEnd = false;
        firstEnmey = 0;
        dungeonWaves = GetComponentsInChildren<DungeonWave>();
        //enemySpawnPoints = GetComponentsInChildren<Transform>();
    }
    Enemy enemy;
    public void ZoneStart()
    {
        dungeon.curZone = this;
        Debug.Log("DungeonZone Start");
        waveCount = dungeonWaves.Length;
        for (int i = 0; i < enemySpawnPoints.Length; i++)
        {
            enemy = Instantiate(enemyPrefab, enemySpawnPoints[i].transform.position, Quaternion.identity);
            enemy.transform.parent = enemySpawnPoints[i].transform;
            firstEnmey++;
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
            if( dungeonWaves[i-1].waveEnd == true && dungeonWaves[i].waveEnd == false)
            {
                dungeonWaves[i].StartWave();
                break;
            }
        }
    }
    public void ZoneClear()
    {
        for (int i = 0; i < enemySpawnPoints.Length; i++)
        {
            enemy.EnemyDelete();
        }
        for (int i = 0; i < dungeonWaves.Length; i++)
        {
            dungeonWaves[i].WaveClear();
        }
    }

    public void KilledEnemy(Enemy e)
    {
        firstEnmey--;
        Debug.Log(firstEnmey);
        if (firstEnmey == 0)
        {
            Debug.Log("FirstEnemy End");
            dungeonWaves[0].StartWave();
        }
    }

    //1. 첫웨이브에서 두번째 가는 기준설정
    //2. 마지막 웨이브 클리어 했는지 체크 - 던전존 클리어 로그 출력
    //3. 플레이가 될 때 까지 구현
}
