using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StuffFactoryBase : MonoBehaviour
{
    public abstract Stuff Instantiate(string instantiateJson);
}

public struct StuffConfig
{
    public StuffFactoryBase stuffFactory;
    public string instantiateJson;

    public Stuff Instantiate()
    {
        return stuffFactory.Instantiate(instantiateJson);
    }
}

public abstract class StuffFactoryBase<SelfType, InstantiateDataType> : StuffFactoryBase 
    where SelfType : StuffFactoryBase<SelfType, InstantiateDataType>
{
    public static SelfType Instance;

    protected virtual void Awake()
    {
        Instance = (SelfType)this;
    }

    public override Stuff Instantiate(string instantiateDataJson)
    {
        return Instantiate(JsonUtility.FromJson<InstantiateDataType>(instantiateDataJson));
    }

    public abstract Stuff Instantiate(InstantiateDataType instantiateData);
}
