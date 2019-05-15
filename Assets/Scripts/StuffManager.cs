using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffManager : MonoBehaviour
{
    public StuffImage stuffImagePrefabs;
    public StuffContentData[] stuffContentDatas;

    private void Start()
    {
        foreach (var stuffContentData in stuffContentDatas)
        {
            float y = stuffContentData.firstOneY;
            foreach (var stuffData in stuffContentData.stuffDatas)
            {
                StuffImage stuffImage = Instantiate(stuffImagePrefabs);
                stuffImage.transform.SetParent(stuffContentData.targetContent);
                stuffImage.StuffData = stuffData;
                RectTransform rect = stuffImage.GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector2(0, y);
                y -= rect.rect.height + 12;
            }
        }
    }

    [System.Serializable]
    public struct StuffContentData
    {
        public RectTransform targetContent;
        public float firstOneY;
        public StuffData[] stuffDatas;
    }
}
