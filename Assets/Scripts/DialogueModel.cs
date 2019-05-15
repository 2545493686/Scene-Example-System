using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class DialogueModel : MonoBehaviour
{
    readonly string c_ConfigPath = System.Environment.CurrentDirectory + "\\Dialogue Config";

    Dictionary<string, string> m_NameDialoguePairs;

    private void Start()
    {
        if (m_NameDialoguePairs == null)
        {
            GetModelFromFile();
        }
    }

    private void GetModelFromFile()
    {
        if (!Directory.Exists(c_ConfigPath))
        {
            Directory.CreateDirectory(c_ConfigPath);
        }

        m_NameDialoguePairs = new Dictionary<string, string>();

        DirectoryInfo root = new DirectoryInfo(c_ConfigPath);
        FileInfo[] files = root.GetFiles("*.txt");

        foreach (FileInfo item in files)
        {
            m_NameDialoguePairs.Add(item.Name.Substring(0, item.Name.Length - 3), File.ReadAllText(item.FullName, Encoding.GetEncoding("gb2312")));
        }
    }

    public string[] GetAllDialogueTitles()
    {
        if (m_NameDialoguePairs == null)
        {
            GetModelFromFile();
        }

        string[] ret = new string[m_NameDialoguePairs.Count];

        int i = 0;
        foreach (var item in m_NameDialoguePairs.Keys)
        {
            ret[i++] = item;
        }

        return ret;
    }

    public string GetDialogue(string title)
    {
        return m_NameDialoguePairs[title];
    }

    private string GetStuffConfigPath(StuffData stuffData)
    {
        return c_ConfigPath + "\\" + stuffData.name + ".txt";
    }

    [System.Serializable]
    struct DialogueData
    {
        public string title;
        public string content;
    }
}
