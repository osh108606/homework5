using UnityEngine;
using UnityEngine.UIElements;

public class DungeonZone : MonoBehaviour
{
    public string key;
    public Transform playerRespawnPoint;
    //���� ���� �÷��̾� ����������Ʈ
    public Transform[] enemySpawnPoint;
    //�������� ���� �� ��������Ʈ
    public Transform[] enemyWavespawnPoint;
    //�������� ���̺� �� ��������Ʈ

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
    //1. ù���̺꿡�� �ι�° ���� ���ؼ���
    //2. ������ ���̺� Ŭ���� �ߴ��� üũ - ������ Ŭ���� �α� ���
    //3. �÷��̰� �� �� ���� ����
}
