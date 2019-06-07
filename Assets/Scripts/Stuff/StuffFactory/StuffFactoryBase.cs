using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StuffFactoryBase : MonoBehaviour
{
    public abstract Stuff Instantiate(string stuffConfigJson);
}
