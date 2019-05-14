﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class StageMaster : MonoBehaviour
{
    public RectTransform stageContent;
    public Text notingText;

    public static StageMaster Instance;

    Transform m_StageStuffsParents;
    Image m_StageImage;

    private void Start()
    {
        Instance = this;

        InitializeStages();

        InitializeStuffs();

        m_StageImage = GetComponent<Image>();
    }

    private void InitializeStuffs()
    {
        //foreach (var stuff in stuffContents)
        //{
        //    for (int i = 0; i < stuff.childCount; i++)
        //    {
        //        stuff.GetChild(i).gameObject.AddComponent<StuffImage>().StageMaster = this;
        //    }
        //}

        GameObject stuffs = new GameObject("Stuffs");
        stuffs.transform.SetParent(transform);
        m_StageStuffsParents = stuffs.transform;
    }

    private void InitializeStages()
    {
        for (int i = 0; i < stageContent.childCount; i++)
        {
            stageContent.GetChild(i).gameObject.AddComponent<Stage>().StageMaster = this;
        }
    }

    public void SetStage(Stage stage)
    {
        notingText.gameObject.SetActive(false);
        m_StageImage.sprite = stage.Image.sprite;
    }

    public void AddStuff(Sprite sprite, string name)
    {
        GameObject @object = new GameObject(name, typeof(DraggableStuff));
        @object.transform.SetParent(m_StageStuffsParents);
        @object.AddComponent<Image>().sprite = sprite;
        @object.GetComponent<RectTransform>().position = m_StageImage.GetComponent<RectTransform>().position;
        @object.GetComponent<RectTransform>().localScale *= 2;
    }

    public void Clear()
    {
        notingText.gameObject.SetActive(true);

        for (int i = 0; i < m_StageStuffsParents.childCount; i++)
        {
            Destroy(m_StageStuffsParents.GetChild(i).gameObject);
        }
    }

}
