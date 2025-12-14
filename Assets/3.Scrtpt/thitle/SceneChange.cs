using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
    //½ÇÆÐ
    public void StartBoutton()
    {
        SceneManager.LoadScene("MainScene");
        UserManager.instance.userData = null;   
    }
    public void LoadBoutton()
    {
        SceneManager.LoadScene("MainScene");
        UserManager.instance.userData = SaveManager.LoadData<UserData>("UserData.json");
    }
}
