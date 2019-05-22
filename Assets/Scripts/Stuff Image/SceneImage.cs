using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SceneImage : StuffImage
{
    protected override void OnPointerClick()
    {
        StageMaster.Instance.SetStage(Data.texture);
    }
}
