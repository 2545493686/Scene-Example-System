using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    ConfigManager m_Instance;

    static Dictionary<string, IConfigModel> m_NameConfigPairs;

    private void Awake()
    {
        m_Instance = this;

        m_NameConfigPairs = new Dictionary<string, IConfigModel>();
        foreach (var item in GetComponents<IConfigModel>())
        {
            m_NameConfigPairs.Add(item.GetFolderName(), item);
        }
    }

    public static string[] GetAllTitles(string folderName)
    {
        if (m_NameConfigPairs.TryGetValue(folderName, out IConfigModel model))
        {
            return model.GetAllTitles();
        }
        LogErrorNullFolderName(folderName);
        return null;
    }

    public static object GetConfig(string folderName, string fileName)
    {
        if (m_NameConfigPairs.TryGetValue(folderName, out IConfigModel model))
        {
            return model.GetConfig(fileName);
        }
        LogErrorNullFolderName(folderName);
        return null;
    }

    public static IConfigModel GetConfigModel(string folderName)
    {
        if (m_NameConfigPairs.TryGetValue(folderName, out IConfigModel model))
        {
            return model;
        }
        LogErrorNullFolderName(folderName);
        return null;
    }

    private static void LogErrorNullFolderName(string folderName)
    {
        Debug.LogError($"配置文件名：{folderName}不存在！");
    }
}
