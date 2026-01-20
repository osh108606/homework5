using UnityEngine;
using System.Collections;
public class Dungeon : MonoBehaviour
{
    
    public string key;
    //던전구분
    public Transform playerSpawnPoint;
    //던전이동포인트&첫리스폰포인트
    
    public DungeonZone[] zones;
    public DungeonZone curZone;
    public int zoneCount;
    [SerializeField]
    public UserDungeon userDungeon;
    
    //던전내 구역
    public void Awake()
    {
        zoneCount = 0;
        zones = GetComponentsInChildren<DungeonZone>();
    }

    public void DungeonStart()
    {
        DungeonManager.instance.curDungeon = this;
        Debug.Log("DungeonStart");
        userDungeon = UserManager.instance.GetUserDungeon(key);
        zoneCount = zones.Length;
        zones[0].GetComponent<DungeonZone>().ZoneStart();
        userDungeon.tryCount++;
    }
    public void DungeonEnd()
    {
        DungeonManager.instance.curDungeon = null;
        Debug.Log("DungeonEnd");
        userDungeon.clearCount++;
        //보상
        //던전 비활성화
    }

    public void DungeonFail()
    {
        curZone.ZoneStart();
        int cur = curZone.order;
        Player.Instance.Reborn();
        Player.Instance.transform.position = zones[cur].playerRespawnPoint.position;
    }

    public void ZoneEnd()
    {
        zoneCount--;
        Debug.Log("Zone End");
        for (int i = 1; i < zones.Length; i++)
        {
            if (zones[i - 1].zoneEnd && zones[i].zoneEnd == false)
            {
                zones[i].ZoneStart();
                break;
            }
        }
        if (zoneCount == 0)
        {
            DungeonEnd();
        }
    }

}
