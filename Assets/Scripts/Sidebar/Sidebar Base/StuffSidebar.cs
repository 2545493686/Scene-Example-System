using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StuffModel))]
public class StuffSidebar : SiderbarBase
{
    [Range(0, 1)]
    public float stuffScreenRatio = 0.05f;

    StuffModel m_StuffModel;

    bool m_LoadFlag = false;

    private void Start()
    {
        m_StuffModel = GetComponent<StuffModel>();
    }

    private void Update()
    {
        if (!m_LoadFlag)
        {
            if (m_StuffModel.IsLoaded)
            {
                List<StuffImageData> stuffDatas = new List<StuffImageData>();
                foreach (var item in m_StuffModel.GetAllTitles())
                {
                    Debug.Log(item);
                    stuffDatas.Add(new StuffImageData
                    {
                        name = item,
                        sprite = m_StuffModel.GetData(item)
                    });
                }
                //SetContentParentHeight(stuffDatas.Count);
                foreach (var item in CreateStuffImages(stuffDatas.ToArray()))
                {
                    item.ScreenRatio = stuffScreenRatio;
                }

                m_LoadFlag = true;
            }
        }
    }
}
