﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenesSidebar : SidebarBase<SceneImage>
{
    public SceneImage sceneImage;

    public StuffImageData[] sceneImageDatas;

    public override SceneImage StuffImagePrefabs => sceneImage;

    private void Awake()
    {
        CreateStuffImages(sceneImageDatas);
    }
}