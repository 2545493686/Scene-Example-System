using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffSidebar : MonoBehaviour
{
    public StuffImage stuffImagePrefabs;

    public RectTransform targetContent;
    public float firstOneY;
    public StuffData[] stuffDatas;

    private void Start()
    {
        float y = firstOneY;
        foreach (var stuffData in stuffDatas)
        {
            y = CreateStuffImage(y, stuffData);
        }
    }

    private float CreateStuffImage(float y, StuffData stuffData)
    {
        StuffImage stuffImage = Instantiate(stuffImagePrefabs);
        stuffImage.transform.SetParent(targetContent);
        stuffImage.StuffData = stuffData;
        RectTransform rect = stuffImage.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(0, y);
        y -= rect.rect.height + 12;
        return y;
    }
}
