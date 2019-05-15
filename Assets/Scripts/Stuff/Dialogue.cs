using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : Stuff
{
    public void SetText(string text)
    {
        transform.Find("Text").GetComponent<Text>().text = text;
    }
}
