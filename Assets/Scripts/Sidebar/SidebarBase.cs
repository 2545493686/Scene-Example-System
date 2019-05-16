using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiderbarBase : SidebarBase<StuffImage>
{
    public StuffImage stuffImagePrefabs;
    public override StuffImage StuffImagePrefabs => stuffImagePrefabs;
}

public abstract class SidebarBase<T> : MonoBehaviour where T : StuffImage
{
    public abstract T StuffImagePrefabs { get; }

    public RectTransform targetContent;
    public float firstOneY = 94;
    public float spacing = 12;


    protected void SetContentParents(int stuffsCount)
    {
        targetContent.sizeDelta = new Vector2
        {
            x = targetContent.sizeDelta.x,
            y = (StuffImagePrefabs.GetComponent<RectTransform>().sizeDelta.y + spacing) * stuffsCount
        };
    }

    protected T[] CreateStuffImages(StuffImageData[] stuffDatas)
    {
        float y = firstOneY;

        T[] rets = new T[stuffDatas.Length];

        for (int i = 0; i < stuffDatas.Length; i++)
        {
            StuffImageData stuffData = stuffDatas[i];
            rets[i] = CreateStuffImage(ref y, stuffData);
        }

        return rets;
    }

    private T CreateStuffImage(ref float y, StuffImageData stuffData)
    {
        T stuffImage = Instantiate(StuffImagePrefabs);
        stuffImage.transform.SetParent(targetContent);
        stuffImage.Data = stuffData;
        RectTransform rect = stuffImage.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(0, y);
        y -= rect.rect.height + spacing;
        return stuffImage;
    }
}
