using UnityEngine;

public class DungeonZone : MonoBehaviour
{
    public string key;
    public Transform playerRespawnPoint;
    //던전 구역 플레이어 리스폰포인트
    public Transform[] enemySpawnPoint;
    //던전구역 시작 적 스폰포인트
    public Transform[] enemyWavespawnPoint;
    //던전구역 웨이브 적 스폰포인트
    public bool zoneEnd;

    public void ZoneStart()
    {
        zoneEnd = false;
    }
    public bool ZoneEnd()
    {
        zoneEnd = true;
        return zoneEnd;
    }
}
