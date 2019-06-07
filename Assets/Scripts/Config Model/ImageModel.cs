using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class ImageModel : ConfigModelBase<Texture>
{
    public string configFolderName;
    public string[] searchPatterns = new string[]
    {
        "*.png",
        "*.jpg"
    };

    protected override string FolderName => configFolderName;

    protected override Dictionary<string, Texture> Datas => datas;
    Dictionary<string, Texture> datas;

    Texture m_TextureData;

    protected override void Initialize(UnityEvent onInitialized)
    {
        StartCoroutine(InitializeCoroutine(onInitialized));
    }

    protected IEnumerator InitializeCoroutine(UnityEvent onInitialized)
    {
        datas = new Dictionary<string, Texture>();

        foreach (var searchPattern in searchPatterns)
        {
            var files = GetAllFiles(searchPattern);

            foreach (var fileInfo in files)
            {
                yield return GetSpirteData(fileInfo);

                datas.Add
                (
                    key: fileInfo.Name.Substring(0, fileInfo.Name.Length - searchPattern.Length + 1), 
                    value: m_TextureData
                );
            }
        }

        onInitialized.Invoke();
    }

    private IEnumerator GetSpirteData(FileInfo fileInfo)
    {
        UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(@"file://" + fileInfo.FullName);
        yield return webRequest.SendWebRequest();
        m_TextureData = DownloadHandlerTexture.GetContent(webRequest);
        //m_TextureData = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    }
}
