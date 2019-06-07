using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public interface IConfigModel
{
    object GetConfig(string title);
    string GetFolderName();
    string[] GetAllTitles();
    void AddInitializedAction(UnityAction action);
}

public abstract class ConfigModelBase<T> : MonoBehaviour, IConfigModel
{
    protected abstract string FolderName { get; }

    protected abstract Dictionary<string, T> Datas { get; }

    protected string ConfigPath
    {
        get
        {
            if (!Directory.Exists(_ConfigPath))
            {
                Directory.CreateDirectory(_ConfigPath);
            }

            if (!Directory.Exists(_ConfigPath + "\\" + FolderName))
            {
                Directory.CreateDirectory(_ConfigPath + "\\" + FolderName);
            }

            return _ConfigPath + "\\" + FolderName;
        }
    }

    UnityEvent m_OnInitialized = new UnityEvent();
    string _ConfigPath = System.Environment.CurrentDirectory + "\\Config";

    protected void Awake()
    {
        Initialize(m_OnInitialized);
    }

    protected abstract void Initialize(UnityEvent onInitialized);

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

    public T GetConfig(string title)
    {
        return Datas[title];
    }

    protected FileInfo[] GetAllFiles(string searchPattern)
    {
        return new DirectoryInfo(ConfigPath).GetFiles(searchPattern);
    }

    #region IConfigModel

    object IConfigModel.GetConfig(string title)
    {
        return GetConfig(title);
    }

    string IConfigModel.GetFolderName()
    {
        return FolderName;
    }

    string[] IConfigModel.GetAllTitles()
    {
        return GetAllTitles();
    }

    void IConfigModel.AddInitializedAction(UnityAction action)
    {
        m_OnInitialized.AddListener(action);
    }

    #endregion
}

