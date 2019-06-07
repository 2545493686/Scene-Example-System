using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ArrayImage : MonoBehaviour, IPointerDownHandler
{
    public OnArrayImageClick onClick = new OnArrayImageClick();

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        onClick.Invoke(eventData);
    }
    public class OnArrayImageClick : UnityEvent<PointerEventData> { }
}
