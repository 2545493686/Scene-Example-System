using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class ConfigModelBase<T> : MonoBehaviour
{
    protected abstract string ConfigFolderName { get; }
    protected abstract InitializeData[] Initialize();

    Dictionary<string, T> NameDataPairs
    {
        get
        {
            if (_NameDataPairs == null)
            {
                _NameDataPairs = new Dictionary<string, T>();
                foreach (var item in Initialize())
                {
                    _NameDataPairs.Add(item.title, item.data);
                }
            }

            return _NameDataPairs;
        }
    }
    Dictionary<string, T> _NameDataPairs;

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
                Directory.CreateDirectory(ConfigFolderName);
            }

            return _ConfigPath + "\\" + ConfigFolderName;
        }
    }
    string _ConfigPath = System.Environment.CurrentDirectory + "\\Config";

    public string[] GetAllTitles()
    {
        string[] ret = new string[NameDataPairs.Count];

        int i = 0;
        foreach (var item in NameDataPairs.Keys)
        {
            ret[i++] = item;
        }

        return ret;
    }

    public T GetData(string title)
    {
        return NameDataPairs[title];
    }

    protected struct InitializeData
    {
        public string title;
        public T data; 
    }
}
