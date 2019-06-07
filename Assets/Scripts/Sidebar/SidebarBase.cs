using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiderbarBase : SidebarBase<ImageGrid>
{

    public ImageGrid stuffImagePrefabs;
    public override ImageGrid StuffImagePrefabs => stuffImagePrefabs;
}

public abstract class SidebarBase<T> : MonoBehaviour where T : ImageGrid
{
    public abstract T StuffImagePrefabs { get; }

    public string configFolderName;
    public RectTransform targetContent;
    public float firstOneY = -53;
    public float spacing = 12;


    private void SetContentParentHeight(int stuffsCount)
    {
        targetContent.sizeDelta = new Vector2
        {
            x = targetContent.sizeDelta.x,
            y = (StuffImagePrefabs.GetComponent<RectTransform>().sizeDelta.y + spacing) * stuffsCount
        };
    }

    protected T[] CreateStuffImages(ImageGridData[] stuffDatas)
    {
        SetContentParentHeight(stuffDatas.Length);

        float y = firstOneY;

        T[] rets = new T[stuffDatas.Length];

        for (int i = 0; i < stuffDatas.Length; i++)
        {
            ImageGridData stuffData = stuffDatas[i];
            rets[i] = CreateStuffImage(ref y, stuffData);
        }

        return rets;
    }

    private T CreateStuffImage(ref float y, ImageGridData stuffData)
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
