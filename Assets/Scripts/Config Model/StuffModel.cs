using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class StuffModel : ConfigModelBase<Sprite>
{
    public string[] searchPatterns = new string[]
    {
        "*.png",
        "*.jpg"
    };

    public bool IsLoaded { get; private set; }

    protected override string ConfigFolderName => "Stuff";

    protected override Dictionary<string, Sprite> Datas => datas;
    Dictionary<string, Sprite> datas;

    Sprite m_SpirteData;

    private void Awake()
    {
        StartCoroutine(Initialize());
    }

    protected IEnumerator Initialize()
    {
        datas = new Dictionary<string, Sprite>();

        foreach (var searchPattern in searchPatterns)
        {
            var files = GetAllFiles(searchPattern);

            foreach (var fileInfo in files)
            {
                yield return GetSpirteData(fileInfo);

                datas.Add
                (
                    key: fileInfo.Name.Substring(0, fileInfo.Name.Length - searchPattern.Length + 1), 
                    value: m_SpirteData
                );
            }
        }

        IsLoaded = true;
    }

    private IEnumerator GetSpirteData(FileInfo fileInfo)
    {
        UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(@"file://" + fileInfo.FullName);
        yield return webRequest.SendWebRequest();
        Texture2D texture = DownloadHandlerTexture.GetContent(webRequest);
        m_SpirteData = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    }

    private void Start()
    {
        Initialize();
    }
}
