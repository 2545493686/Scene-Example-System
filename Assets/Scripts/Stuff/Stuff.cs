using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[System.Serializable]
public struct StuffData
{
    public string fileName;
    public Texture texture;

    public string GetRealName()
    {
        return ConfigManager.GetRealName(fileName);
    }
}

[RequireComponent(typeof(RectTransform))]
public class Stuff : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{
    public StuffFactoryBase StuffFactory { get; set; }
    public static Stuff SelectedStuff { get; set; }

    public StuffData StuffData { get; set; }

    protected RectTransform RectTransform
    {
        get
        {
            if (!_RectTransform)
            {
                _RectTransform = GetComponent<RectTransform>();
            }
            return _RectTransform;
        }
    }

    RectTransform _RectTransform;

    Vector3 m_SelectedPosition;
    Vector3 m_Revise;
    bool m_Selected = false;
    bool m_IsPointerEnter = false;


    public virtual string ToJson()
    {
        return JsonUtility.ToJson(new StuffConfig
        {
            stuffFactory = StuffFactory,
            stuffData = StuffData,
            screenRatio = 
        });
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        SelectedStuff = this;
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
        //m_Selected = false;
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        m_Selected = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        TryDestroy();

        TryMove();
    }

    private void TryDestroy()
    {
        if (m_IsPointerEnter && m_Selected && (Input.GetKey(KeyCode.Delete) || Input.GetKey(KeyCode.Mouse1)))
        {
            DestroySelf();
        }
    }

    protected virtual void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void TryMove()
    {
        if (m_Selected && Input.mousePosition != m_SelectedPosition)
        {
            RectTransform.position = Input.mousePosition + m_Revise;
        }
    }
}
