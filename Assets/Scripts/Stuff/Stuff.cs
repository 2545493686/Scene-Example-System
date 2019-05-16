using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class Stuff : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{
    public StuffImageData StuffData { get; set; }

    RectTransform m_RectTransform;
    Vector3 m_SelectedPosition;
    Vector3 m_Revise;
    bool m_Selected = false;
    bool m_IsPointerEnter = false;

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        m_Selected = true;
        m_SelectedPosition = eventData.pressPosition;
        m_Revise = transform.position - m_SelectedPosition;
    }
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        m_IsPointerEnter = true;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        m_IsPointerEnter = false;
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        m_Selected = false;
    }

    protected virtual void Start()
    {
        m_RectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (m_IsPointerEnter && (Input.GetKey(KeyCode.Delete) || Input.GetKey(KeyCode.Mouse1)))
        {
            Destroy(gameObject);
        }
        if (m_Selected && Input.mousePosition != m_SelectedPosition)
        {
            m_RectTransform.position = Input.mousePosition + m_Revise;
        }
    }
}
