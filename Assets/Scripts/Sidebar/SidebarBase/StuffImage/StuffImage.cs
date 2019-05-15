using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
public struct StuffData
{
    public string name;
    public Sprite sprite;
}

public class StuffImage : MonoBehaviour, IPointerDownHandler
{
    public StuffData StuffData
    {
        get => _StuffData;
        set
        {
            _StuffData = value;
            gameObject.name = StuffData.name;

            SetText();
            SetImage();
        }
    }
    StuffData _StuffData;

    private void SetImage()
    {
        if (StuffData.sprite)
        {
            transform.Find("Image").GetComponent<Image>().sprite = StuffData.sprite;
        }
    }

    private void SetText()
    {
        var text = transform.Find("Text").GetComponent<Text>();
        text.text = StuffData.name;
        if (!StuffData.sprite)
        {
            text.alignment = TextAnchor.MiddleCenter;
            text.fontSize = 23;
        }
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        OnPointerDown();
    }

    protected virtual void OnPointerDown()
    {
        GameObject stuff = new GameObject(StuffData.name);

        stuff.AddComponent<Stuff>().StuffData = StuffData;

        Image image = stuff.AddComponent<Image>();
        image.sprite = StuffData.sprite;
        image.SetNativeSize();

        StageMaster.Instance.Add(image, 0.03f);
    }
}
