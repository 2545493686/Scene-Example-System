using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DialogueManager : MonoBehaviour
{
    public StuffManager stuffManager;

    readonly string c_ConfigPath = System.Environment.CurrentDirectory + "\\Dialogue Config";

    Dictionary<string, string> nameDialoguePairs;

    private void Start()
    {
        if (!Directory.Exists(c_ConfigPath))
        {
            Directory.CreateDirectory(c_ConfigPath);
        }

        nameDialoguePairs = new Dictionary<string, string>();
        foreach (var stuffContentData in stuffManager.stuffContentDatas)
        {
            foreach (var stuffData in stuffContentData.stuffDatas)
            {
                if (!File.Exists(GetStuffConfigPath(stuffData)))
                {
                    File.Create(GetStuffConfigPath(stuffData));
                    Debug.LogWarning("找不到物体:" + stuffData.name + "的配置! 已重新创建空文件!");
                    nameDialoguePairs.Add(stuffData.name, null);
                }
                else
                {
                    try
                    {
                        var config = File.ReadAllText(GetStuffConfigPath(stuffData));
                        foreach (var item in config.Split('\n'))
                        {
                            if (item.Contains(":::"))
                            {
                                throw new System.Exception("配置文件不正确: " + GetStuffConfigPath(stuffData));
                            }


                        }
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e.Message);
                        nameDialoguePairs.Add(stuffData.name, null);
                    }
                }
            }
        }
    }

    private string GetStuffConfigPath(StuffData stuffData)
    {
        return c_ConfigPath + "\\" + stuffData.name + ".txt";
    }
}
