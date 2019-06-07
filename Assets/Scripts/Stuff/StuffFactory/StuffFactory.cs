using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

interface IStuffConfig
{

}

public struct StuffConfig
{
    public StuffFactoryBase stuffFactory;
    public StuffData stuffData;
}

public class StuffFactory : StuffFactoryBase
{
    [Range(0.01f, 1f)]
    public float screenRatio = 0.05f;

    public override Stuff Instantiate(string stuffConfigJson)
    {
        StuffConfig data = JsonUtility.FromJson<StuffConfig>(stuffConfigJson);

        GameObject @object = new GameObject(data.stuffData.fileName);

        var stuff = @object.AddComponent<Stuff>();
        stuff.StuffData = data.stuffData;
        stuff.StuffFactory = this;

        RawImage image = @object.AddComponent<RawImage>();
        image.texture = data.stuffData.texture;
        image.SetNativeSize();

        @object.transform.localScale *= Screen.height / image.texture.height * data.screenRatio;
        return stuff;
    }
}
