using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffSidebar : SidebarBase
{
    public StuffData[] stuffDatas;

    private void Start()
    {
        CreateStuffImages(stuffDatas);
    }
}
