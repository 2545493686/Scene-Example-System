using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageSidebar : SiderbarBase
{
    [Range(0, 1)]
    public float stuffScreenRatio = 0.05f;

    IConfigModel m_StuffModel;

    private void Start()
    {
        m_StuffModel = ConfigManager.GetConfigModel(configFolderName);
        m_StuffModel.AddInitializedAction(() => 
        {
            List<StuffData> stuffDatas = new List<StuffData>();
            foreach (var item in m_StuffModel.GetAllTitles())
            {
                stuffDatas.Add(new StuffData
                { 
                    fileName = item,
                    texture = (Texture)m_StuffModel.GetConfig(item)
                });
            }
            //SetContentParentHeight(stuffDatas.Count);
            foreach (var item in CreateStuffImages(stuffDatas.ToArray()))
            {
                item.ScreenRatio = stuffScreenRatio;
            }
        });
    }
}
