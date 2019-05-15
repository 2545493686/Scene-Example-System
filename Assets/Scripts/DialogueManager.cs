using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class DialogueManager : MonoBehaviour
{
    public StuffSidebar stuffManager;

    readonly string c_ConfigPath = System.Environment.CurrentDirectory + "\\Dialogue Config";

    Dictionary<string, List<DialogueData>> nameDialoguePairs;

    private void Start()
    {
        if (!Directory.Exists(c_ConfigPath))
        {
            Directory.CreateDirectory(c_ConfigPath);
        }

        nameDialoguePairs = new Dictionary<string, List<DialogueData>>();
        foreach (var stuffData in stuffManager.stuffDatas)
        {
            nameDialoguePairs.Add(stuffData.name, new List<DialogueData>());

            if (!File.Exists(GetStuffConfigPath(stuffData)))
            {
                File.Create(GetStuffConfigPath(stuffData));
                Debug.LogWarning("找不到物体:" + stuffData.name + "的配置! 已重新创建空文件!");
            }
            else
            {
                try
                {
                    var config = File.ReadAllText(GetStuffConfigPath(stuffData), Encoding.GetEncoding("gb2312"));

                    foreach (var line in config.Split('\n'))
                    {
                        if (!line.Contains(":::"))
                        {
                            throw new System.Exception("配置文件不正确: " + GetStuffConfigPath(stuffData));
                        }
                        else
                        {
                            var splitIndex = line.IndexOf(":::");
                            DialogueData dialogueData = new DialogueData
                            {
                                title = line.Substring(0, splitIndex),
                                content = line.Substring(splitIndex + 3, line.Length - splitIndex - 3)
                            };
                            nameDialoguePairs[stuffData.name].Add(dialogueData);
                        }
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e.Message);
                }
            }
        }

        foreach (var stuff in nameDialoguePairs)
        {
            foreach (var item in stuff.Value)
            {
                Debug.Log($"title: {item.title}, content: {item.content}");
            }
        }
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
