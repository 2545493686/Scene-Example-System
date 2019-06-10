using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStuffFromJson
{
    Stuff Instantiate(string instantiateJson);
}

public struct StuffConfig
{
    public IStuffFromJson stuffFactory;
    public string instantiateJson;

    public Stuff Instantiate()
    {
        return stuffFactory.Instantiate(instantiateJson);
    }
}

public abstract class StuffFactoryBase<SelfType, InstantiateDataType> : MonoBehaviour, IStuffFromJson 
    where SelfType : StuffFactoryBase<SelfType, InstantiateDataType>
{
    public static SelfType Instance;

    protected virtual void Awake()
    {
        Instance = (SelfType)this;
    }

    public Stuff Instantiate(string instantiateDataJson)
    {
        return Instantiate(JsonUtility.FromJson<InstantiateDataType>(instantiateDataJson));
    }

    public abstract Stuff Instantiate(InstantiateDataType instantiateData);
}
