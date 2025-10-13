using UnityEngine;
using UnityEngine.UIElements;

public class DungeonZone : MonoBehaviour
{
    public string key;
    public Transform playerRespawnPoint;
    //던전 구역 플레이어 리스폰포인트
    public Transform[] enemySpawnPoint;
    //던전구역 시작 적 스폰포인트
    public Transform[] enemyWavespawnPoint;
    //던전구역 웨이브 적 스폰포인트

    public EnemySpanwer[] enemySpanwers;
    public GameObject enemyobj;
    public int firstEnmey;

    public bool zoneEnd;

    

    public void ZoneStart()
    {
        zoneEnd = false;
        
        for (int i = 0; i < enemySpawnPoint.Length; i++)
        {

            GameObject enemy = Instantiate(enemyobj, enemySpawnPoint[i].transform.position, Quaternion.identity);
            enemy.transform.parent = transform;
            firstEnmey++;
        }
        
    }

    public void ZoneEnemyDie()
    {
        firstEnmey--;
        if (firstEnmey == 0)
        {
            for (int i = 0; i < enemySpanwers.Length; i++)
            {
                enemySpanwers[i].StartSpawn();
            }
        }
    }

    public bool ZoneEnd()
    {
        zoneEnd = true;
        return zoneEnd;
    }
    //1. 첫웨이브에서 두번째 가는 기준설정
    //2. 마지막 웨이브 클리어 했는지 체크 - 던전존 클리어 로그 출력
    //3. 플레이가 될 때 까지 구현
}
