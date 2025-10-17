using UnityEngine;
using UnityEngine.UIElements;

public class DungeonZone : MonoBehaviour, IEnemySpawner
{
    public string key;
    public Transform playerRespawnPoint;
    //던전 구역 플레이어 리스폰포인트
    public Transform[] enemySpawnPoint;
    //던전구역 시작 적 스폰포인트
    public DungeonWave[] dungeonWaves;


    public Enemy enemyPrefab;
    public int firstEnmey;
    public int waveCount;
    public bool zoneEnd;

    public void Awake()
    {
        zoneEnd = false;

        dungeonWaves = GetComponentsInChildren<DungeonWave>();
    }

    public void ZoneStart()
    {
        zoneEnd = false;
        waveCount = dungeonWaves.Length;
        for (int i = 0; i < enemySpawnPoint.Length; i++)
        {

            Enemy enemy = Instantiate(enemyPrefab, enemySpawnPoint[i].transform.position, Quaternion.identity);
            enemy.transform.parent = transform;
            firstEnmey++;
        }

    }

    public void WaveEnd()
    {
        waveCount--;
        if (waveCount == 0)
        {
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
    

    public void KilledEnemy(Enemy e)
    {
        firstEnmey--;
        if (firstEnmey == 0)
        {
            dungeonWaves[0].StartWave();
        }
    }

    //1. 첫웨이브에서 두번째 가는 기준설정
    //2. 마지막 웨이브 클리어 했는지 체크 - 던전존 클리어 로그 출력
    //3. 플레이가 될 때 까지 구현
}
