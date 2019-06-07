﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SceneImage : ImageGrid
{
    protected override void OnPointerClick()
    {
        SceneMaster.Instance.SetStage(Data.fileName);
    }
}
