using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueImage : StuffImage
{
    protected override void OnPointerDown()
    {
        StageMaster.Instance.AddDialogue("2333333");
    }
}
