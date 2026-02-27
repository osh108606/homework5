using System;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public void StartNewGame()
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
                UserManager.instance.userDataFileName = userSaveData.saveSlots[i].userDataFileName;
            }
        }
        SceneManager.LoadScene("MainScene");
    }

    public void LoadGame()
    {
        //순서1 타이틀1 비활성화 타이틀2 활성화
        for (int i = 0; i < userSaveData.saveSlots.Length; i++)
        {
            if (userSaveData.saveSlots[i] != null || string.IsNullOrEmpty(userSaveData.saveSlots[i].userDataFileName))
            {
                SaveManager.LoadData<SaveSlot>(userSaveData.saveSlots[i].userDataFileName);
                SaveManager.SaveData("UserSaveData", userSaveData);
            }
        }
    }
}
[System.Serializable]
public class UserSaveData
{
    public SaveSlot[] saveSlots = new SaveSlot[4];
}

[System.Serializable]
public class SaveSlot
{
    public int order;
    public string createTime;
    public string saveTime;
    public string userDataFileName;
}