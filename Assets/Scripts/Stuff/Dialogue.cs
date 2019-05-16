using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : Stuff
{
    public virtual void SetText(string text)
    {
        GetComponentInChildren<Text>().text = text;
    }
}
