using UnityEngine;
using UnityEngine.UI;
public class SelectButton : MonoBehaviour
{
    public string key;
    public string preKey;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClickedButton);
    }

    public void OnClickedButton()
    {
        UserDungeon userDungeon = UserManager.instance.GetUserDungeon(preKey);
        if(string.IsNullOrEmpty(preKey) || userDungeon.clearCount > 0)
        {
            Debug.Log("start");
            GetComponentInParent<StageSelect>().SelectedDungeon(key);
        }
    }
}
