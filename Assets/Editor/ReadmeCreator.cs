#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;

public class ReadmeCreator
{
    [MenuItem("Tools/Create README File")]
    public static void CreateReadme()
    {
        string path = Path.Combine(Application.dataPath, "README.md");

        if (!File.Exists(path))
        {
            File.WriteAllText(path, "# ������Ʈ README\n\n���⿡ ������Ʈ ������ �ۼ��ϼ���.");
            Debug.Log("README.md ������ �����Ǿ����ϴ�: " + path);
        }
        else
        {
            Debug.LogWarning("README.md ������ �̹� �����մϴ�.");
        }

        AssetDatabase.Refresh();
    }
}
#endif
