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
        List<string> ret = new List<string>();

        foreach (var item in Datas.Keys)
        {
            ret.Add(item);
        }

        ret.Sort((a, b) =>
        {
            if (!a.Contains("-"))
                return 1;

            if (!b.Contains("-"))
                return -1;

            if (!TryParse(a, out int aNumber))
                return 1;

            if (!TryParse(b, out int bNumber))
                return -1;

            return aNumber.CompareTo(bNumber);
        });

        foreach (var item in ret)
        {
            Debug.Log(item);
        }

        return ret.ToArray();
    }

    private static bool TryParse(string a, out int result)
    {
        return int.TryParse(a.Substring(0, a.IndexOf('-')), out result);
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
