using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StuffFactory : StuffFactoryBase<StuffFactory, StuffFactory.InstantiateData>
{
    public string folderName = "Stuff";
    [Range(0.01f, 1f)]
    public float screenRatio = 0.05f;

    public struct InstantiateData
    {
        public string fileName;
        public Vector3 worldPoint;
    }

    public override Stuff Instantiate(InstantiateData instantiateData)
    {
        GameObject @object = new GameObject(instantiateData.fileName);

        StuffData data = new StuffData
        {
            fileName = instantiateData.fileName,
            texture = (Texture)ConfigManager.GetConfig(folderName, instantiateData.fileName)
        };

        var stuff = @object.AddComponent<Stuff>();
        stuff.Data = data;
        stuff.Factory = this;
        stuff.transform.position = instantiateData.worldPoint;

        RawImage image = @object.AddComponent<RawImage>();
        image.texture = data.texture;
        image.SetNativeSize();

        @object.transform.localScale *= Screen.height / image.texture.height * screenRatio;
        return stuff;
    }
}
