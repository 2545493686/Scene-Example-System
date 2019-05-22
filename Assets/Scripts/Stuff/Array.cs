using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Array : Stuff
{
    public ArrayImage line;
    public ArrayImage head;

    public bool Move { get; set; } = false;

    RectTransform m_ImageRect;

    bool m_Initialized = false;

    private void Start()
    {
        m_ImageRect = line.GetComponent<RectTransform>();

        line.onClick.AddListener(OnClick);
        head.onClick.AddListener(OnClick);
    }

    private void OnClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Destroy(gameObject);
        }
        Move = !Move;
    }

    protected override void Update()
    {
        if (Move)
        {
            Vector3 mouseDirection = Input.mousePosition - transform.position;

            var cosAngle = Vector3.Dot(mouseDirection.normalized, Vector3.up);

            //Debug.Log(Mathf.Rad2Deg * Mathf.Acos(cosAngle));

            transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Acos(cosAngle) * (mouseDirection.x > 0 ? -1 : 1));

            m_ImageRect.sizeDelta = new Vector2
            {
                x = m_ImageRect.sizeDelta.x,
                y = mouseDirection.magnitude
            };

            m_ImageRect.anchoredPosition = new Vector2
            {
                x = m_ImageRect.anchoredPosition.x,
                y = mouseDirection.magnitude * 0.5f
            };

            head.GetComponent<RectTransform>().anchoredPosition = new Vector2
            {
                x = head.GetComponent<RectTransform>().anchoredPosition.x,
                y = mouseDirection.magnitude - 5
            };

            if (!m_Initialized)
            {
                foreach (var image in GetComponentsInChildren<Image>())
                {
                    image.enabled = true;
                }
                m_Initialized = true;
            }
        }


        //base.Update();
    }
}
