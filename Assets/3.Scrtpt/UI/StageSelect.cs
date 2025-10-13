using UnityEngine;
using UnityEngine.UI;


public class StageSelect : MonoBehaviour
{
    public static StageSelect instance;
    public Button[] buttons;
    public Dungeon[] dungeons;
    private void Awake()
    {
        instance = this;
    }

    public void OnClicked(string key)
    {
        Debug.Log("OnCliced");
        for (int i = 0; i < dungeons.Length; i++)
        {
            if (dungeons[i].key == key)
            {
                Debug.Log("OnCliced if (dungeons[i].key == key)");
                Player.instance.transform.position = dungeons[i].playerSpawnPoint.transform.position;
                dungeons[i].DungeonStart();
                gameObject.SetActive(false);

           
            }
        }
       
    }
}
