using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayFactory : StuffFactoryBase<ArrayFactory, ArrayFactory.InstantiateData>
{
    public Array arrayPrefab;

    public struct InstantiateData
    {
        public bool isMoving;
        public Vector3 endPoint;
        public Vector3 worldPoint;
    }

    public override Stuff Instantiate(InstantiateData instantiateData)
    {
        var array = Instantiate(arrayPrefab);
        array.Factory = this;
        array.EndPoint = instantiateData.endPoint;
        array.Move = instantiateData.isMoving;
        array.transform.position = instantiateData.worldPoint;
        return array;
    }
}
