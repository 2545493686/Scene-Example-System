using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Toast : MonoBehaviour
{
    public Text text;
    public float showY = 25;
    public float hideY = -25;

    RectTransform m_Rect;
    bool m_IsShowed = false;

    public delegate void Action();

    private void Start()
    {
        m_Rect = GetComponent<RectTransform>();
    }

    public void Show(string text)
    {
        StopAllCoroutines();
        if (m_IsShowed)
        {
            Hide(() =>
            {
                Show(text);
            });
        }
        else
        {
            Debug.Log("show");
            m_IsShowed = true;
            this.text.text = text;
            StartCoroutine(MoveUI(showY, () => StartCoroutine(DelayHide())));
        }
    }

    private void Hide(Action onHided = null)
    {
        Debug.Log("hide");
        StopAllCoroutines();
        m_IsShowed = false;
        StartCoroutine(MoveUI(hideY, onHided));
    }

    IEnumerator DelayHide()
    {
        yield return new WaitForSeconds(0.8f);
        Hide();
    }

    IEnumerator MoveUI(float targetY, Action onHided = null)
    {
        Debug.Log("target:" + targetY);
        while (Mathf.Abs(m_Rect.position.y - targetY) > 0.1f)
        {
            m_Rect.position += new Vector3
            {
                y = (targetY - m_Rect.position.y) / 3
            };
            yield return new WaitForSeconds(0.01f);
            Debug.Log(222);
        }
        m_Rect.position = new Vector2
        {
            x = m_Rect.position.x,
            y = targetY
        };
        if (onHided != null)
        {
            onHided();
        }
        Debug.Log("end");
    }
}
