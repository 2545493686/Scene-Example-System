using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct StuffConfig
{
    public IStuffFromJson stuffFactory;
    public string instantiateJson;
}

public class StuffFactory : StuffFactoryBase<StuffFactory.StuffInstantiateData>
{
    public static StuffFactory Instance;

    public string folderName = "Stuff";
    [Range(0.01f, 1f)]
    public float screenRatio = 0.05f;

    public struct StuffInstantiateData
    {
        public string fileName;
    }

    private void Awake()
    {
        Instance = this;
    }

    public override Stuff Instantiate(StuffInstantiateData instantiateData)
    {
        GameObject @object = new GameObject(instantiateData.fileName);

        StuffData data = new StuffData
        {
            fileName = instantiateData.fileName,
            texture = (Texture)ConfigManager.GetConfig(folderName, instantiateData.fileName)
        };

        var stuff = @object.AddComponent<Stuff>();
        stuff.Data = data;
        stuff.StuffFactory = this;

        RawImage image = @object.AddComponent<RawImage>();
        image.texture = data.texture;
        image.SetNativeSize();

        @object.transform.localScale *= Screen.height / image.texture.height * screenRatio;
        return stuff;
    }
}
