using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class ImageGrid : MonoBehaviour, IPointerClickHandler
{
    public float ScreenRatio { get; set; } = 0.05f;

    public StuffData Data
    {
        get => _Data;
        set
        {
            _Data = value;
            gameObject.name = Data.fileName;

            SetText();
            SetImage();
        }
    }
    StuffData _Data;

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
        SceneMaster.Instance.Add(Stuff.Instantiate(Data, ScreenRatio));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnPointerClick();
    }
}
