using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class StuffImage : MonoBehaviour, IPointerDownHandler
{
    public string stuffName;
    public Sprite sprite;

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        StageMaster.Instance.AddStuff(sprite, stuffName);
    }
}
