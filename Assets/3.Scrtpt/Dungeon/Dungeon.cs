using UnityEngine;
using System.Collections;
public class Dungeon : MonoBehaviour
{
    public string key;
    //던전구분
    public Transform playerSpawnPoint;
    //던전이동포인트&첫리스폰포인트
    
    public DungeonZone[] zones;
    //던전내 구역
    public void Awake()
    {
        zones = GetComponentsInChildren<DungeonZone>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
       
    }

    public void DungeonStart()
    {
        zones[0].GetComponent<DungeonZone>().ZoneStart();
    }
    public void DungeonEnd()
    {

    }
}
