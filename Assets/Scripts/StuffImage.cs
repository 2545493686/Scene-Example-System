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
            transform.Find("Image").GetComponent<Image>().sprite = StuffData.sprite;
            transform.Find("Text").GetComponent<Text>().text = StuffData.name;
        }
    }
    public StuffData _StuffData;

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        StageMaster.Instance.AddStuff(StuffData);
    }
}
