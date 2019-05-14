using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Stage : MonoBehaviour, IPointerClickHandler
{
    public StageMaster StageMaster;
    public Image Image;

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        StageMaster.SetStage(this);
    }

    private void Start()
    {
        Image = GetComponent<Image>();
    }

}
