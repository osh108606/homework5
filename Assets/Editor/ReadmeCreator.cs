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
            File.WriteAllText(path, "# 프로젝트 README\n\n여기에 프로젝트 설명을 작성하세요.");
            Debug.Log("README.md 파일이 생성되었습니다: " + path);
        }
        else
        {
            Debug.LogWarning("README.md 파일이 이미 존재합니다.");
        }

        AssetDatabase.Refresh();
    }
}
#endif
