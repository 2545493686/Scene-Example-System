using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
public struct StuffImageData
{
    public string name;
    public Sprite sprite;
}

public class StuffImage : MonoBehaviour, IPointerClickHandler
{
    readonly float c_ScreenRatio = 0.15f;

    public StuffImageData Data
    {
        get => _Data;
        set
        {
            _Data = value;
            gameObject.name = Data.name;

            SetText();
            SetImage();
        }
    }
    StuffImageData _Data;

    private void SetImage()
    {
        if (Data.sprite)
        {
            transform.Find("Image").GetComponent<Image>().sprite = Data.sprite;
        }
    }

    private void SetText()
    {
        var text = transform.Find("Text").GetComponent<Text>();
        text.text = Data.name;
        if (!Data.sprite)
        {
            text.alignment = TextAnchor.MiddleCenter;
            text.fontSize = 23;
        }
    }

    protected virtual void OnPointerClick()
    {
        GameObject @object = new GameObject(Data.name);

        var stuff = @object.AddComponent<Stuff>();
        stuff.StuffData = Data;

        Image image = @object.AddComponent<Image>();
        image.sprite = Data.sprite;
        image.SetNativeSize();

        @object.transform.localScale *= Screen.height / image.preferredHeight * c_ScreenRatio;

        StageMaster.Instance.Add(stuff);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnPointerClick();
    }
}
