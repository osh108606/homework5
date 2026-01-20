using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager instance;
    public Dungeon curDungeon;
    private void Awake()
    {
        instance = this;
    }
    
}
