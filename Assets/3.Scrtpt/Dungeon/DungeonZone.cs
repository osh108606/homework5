using UnityEngine;

public class DungeonZone : MonoBehaviour
{
    public string key;
    public Transform playerRespawnPoint;
    //���� ���� �÷��̾� ����������Ʈ
    public Transform[] enemySpawnPoint;
    //�������� ���� �� ��������Ʈ
    public Transform[] enemyWavespawnPoint;
    //�������� ���̺� �� ��������Ʈ
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
