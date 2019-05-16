using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class ConfigModelBase<T> : MonoBehaviour
{
    protected abstract string ConfigFolderName { get; }

    protected abstract Dictionary<string, T> Datas { get; }

    protected string ConfigPath
    {
        get
        {
            if (!Directory.Exists(_ConfigPath))
            {
                Directory.CreateDirectory(_ConfigPath);
            }

            if (!Directory.Exists(_ConfigPath + "\\" + ConfigFolderName))
            {
                Directory.CreateDirectory(_ConfigPath + "\\" + ConfigFolderName);
            }

            return _ConfigPath + "\\" + ConfigFolderName;
        }
    }
    string _ConfigPath = System.Environment.CurrentDirectory + "\\Config";

    //protected virtual void Awake()
    //{
    //    StartCoroutine(Initialize());
    //}

    //private IEnumerator Initialize()
    //{
    //    yield return Initialize(out InitializeData[] initializeDatas);

    //    Datas = new Dictionary<string, T>();
    //    foreach (var item in initializeDatas)
    //    {
    //        Datas.Add(item.title, item.data);
    //    }
    //}

    public string[] GetAllTitles()
    {
        string[] ret = new string[Datas.Count];

        int i = 0;
        foreach (var item in Datas.Keys)
        {
            ret[i++] = item;
        }

        return ret;
    }

    public T GetData(string title)
    {
        return Datas[title];
    }

    protected FileInfo[] GetAllFiles(string searchPattern)
    {
        return new DirectoryInfo(ConfigPath).GetFiles(searchPattern);
    }

    //protected struct InitializeData
    //{
    //    public string title;
    //    public T data; 
    //}
}
