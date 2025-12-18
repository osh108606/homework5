using System;
using UnityEngine;

public class TitleSceneManager : MonoBehaviour
{
    public static TitleSceneManager instance;
    public UserSaveData userSaveData;
    private void Awake()
    {
        instance = this;
        userSaveData = SaveManager.LoadData<UserSaveData>("UserSaveData");
        if (userSaveData == null)
        {
            userSaveData = new UserSaveData();

            SaveManager.SaveData("UserSaveData", userSaveData);
        }
    }

    private void Start()
    {
        SaveManager.SaveData("UserSaveData",userSaveData);
    }

    public int StartNewGame()
    {
        for(int i = 0; i < userSaveData.saveSlots.Length;i++)
        {
            if (userSaveData.saveSlots[i] == null || string.IsNullOrEmpty( userSaveData.saveSlots[i].userDataFileName))
            {
                userSaveData.saveSlots[i] = new SaveSlot();
                userSaveData.saveSlots[i].order = i;
                userSaveData.saveSlots[i].createTime = DateTime.Now.ToString();
                userSaveData.saveSlots[i].userDataFileName = DateTime.Now.ToString();
                SaveManager.SaveData("UserSaveData", userSaveData);
                DontDestroyOnLoad(gameObject);
                return i;
            }
        }
        return 0;
    }

    public int LoadGame()
    {
        for (int i = 0; i < userSaveData.saveSlots.Length; i++)
        {
            if (userSaveData.saveSlots[i] != null || string.IsNullOrEmpty(userSaveData.saveSlots[i].userDataFileName))
            {
                SaveManager.LoadData<SaveSlot>(userSaveData.saveSlots[i].userDataFileName);
                SaveManager.SaveData("UserSaveData", userSaveData);
                DontDestroyOnLoad(gameObject);
                return i;
            }
        }
        return 0;
    }
}

public class UserSaveData
{
    public SaveSlot[] saveSlots = new SaveSlot[4];
}

public class SaveSlot
{
    public int order;
    public string createTime;
    public string saveTime;

    public string userDataFileName;
}