using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
public struct ImageGridData
{
    public string title;
    public Texture texture;

    public string GetRealName()
    {
        if (title.Contains("-"))
        {
            if (int.TryParse(title.Substring(0, title.IndexOf('-')), out int result))
            {
                return title.Substring(title.IndexOf('-') + 1, title.Length - (title.IndexOf('-') + 1));
            }
        }
        return title;
    }
}

public class ImageGrid : MonoBehaviour, IPointerClickHandler
{
    public float ScreenRatio { get; set; } = 0.05f;

    public ImageGridData Data
    {
        get => _Data;
        set
        {
            _Data = value;
            gameObject.name = Data.title;

            SetText();
            SetImage();
        }
    }
    ImageGridData _Data;

    private void SetImage()
    {
        if (Data.texture)
        {

            RawImage image = GetComponentInChildren<RawImage>();

            RectTransform rect = image.GetComponent<RectTransform>();

            if (Data.texture.height > Data.texture.width)
            {
                rect.sizeDelta = new Vector2
                {
                    x = Data.texture.width / (Data.texture.height / rect.sizeDelta.y),
                    y = rect.sizeDelta.y
                };
            }
            else
            {
                rect.sizeDelta = new Vector2
                {
                    x = rect.sizeDelta.x,
                    y = Data.texture.height / (Data.texture.width / rect.sizeDelta.x)
                };
            }

            image.texture = Data.texture;

            //image.SetNativeSize();
        }
    }

    private void SetText()
    {
        var text = transform.Find("Text").GetComponent<Text>();

        text.text = Data.GetRealName();

        if (!Data.texture)
        {
            text.alignment = TextAnchor.MiddleCenter;
            text.fontSize = 23;
        }
    }

    protected virtual void OnPointerClick()
    {
        GameObject @object = new GameObject(Data.title);

        var stuff = @object.AddComponent<Stuff>();
        stuff.StuffData = Data;

        RawImage image = @object.AddComponent<RawImage>();
        image.texture = Data.texture;
        image.SetNativeSize();

        @object.transform.localScale *= Screen.height / image.texture.height * ScreenRatio;

        StageMaster.Instance.Add(stuff);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnPointerClick();
    }
}
