﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffSidebar : SiderbarBase
{
    public StuffImageData[] stuffDatas;

    private void Start()
    {
        CreateStuffImages(stuffDatas);
    }
}
