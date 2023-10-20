using UnityEditor;

#if UNITY_EDITOR

public class ProjectFilePostprocessor : AssetPostprocessor
{

    public static string OnGeneratedCSProject(string path, string content)
    {
        content = content.Replace("C:\\", "\\mnt\\c\\");
        content = content.Replace("D:\\", "\\mnt\\d\\");
        return content;
    }
}

#endif
