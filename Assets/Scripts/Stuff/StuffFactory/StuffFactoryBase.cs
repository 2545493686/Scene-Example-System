using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStuffFromJson
{
    Stuff Instantiate(string stuffConfigJson);
}

public abstract class StuffFactoryBase<T> : MonoBehaviour, IStuffFromJson
{
    public Stuff Instantiate(string stuffConfigJson)
    {
        return Instantiate(JsonUtility.FromJson<T>(stuffConfigJson));
    }

    public abstract Stuff Instantiate(T stuffInstantiateData);
}
