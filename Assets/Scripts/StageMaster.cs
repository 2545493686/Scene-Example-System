using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RawImage))]
public class StageMaster : MonoBehaviour, IPointerDownHandler
{
    public Array arrayPrefab;
    public RectTransform stageContent;
    public Text notingText;
    public Text title;

    public static StageMaster Instance;

    Transform m_StageStuffsParents;
    RawImage m_StageImage;

    float clickTime;

    const float c_MaxClickTime = 0.2f;

    private void Start()
    {
        Instance = this;

        //InitializeStages();

        InitializeStuffs();

        m_StageImage = GetComponent<RawImage>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Time.time - clickTime < c_MaxClickTime)
            {
                OnDoubleClick();
            }
            clickTime = Time.time;
        }
    }

    private void OnDoubleClick()
    {
        var array = Instantiate(arrayPrefab);
        array.Move = true;
        array.transform.position = Input.mousePosition;
        Add(array, false);
    }

    private void InitializeStuffs()
    {
        GameObject stuffs = new GameObject("Stuffs");
        stuffs.transform.SetParent(transform);
        m_StageStuffsParents = stuffs.transform;
    }

    public void SetStage(Texture texture, string text)
    {
        notingText.gameObject.SetActive(false);
        m_StageImage.texture = texture;
        title.text = text;
    }

    public void Add(Stuff stuff, bool resetPosition = true)
    {
        RectTransform rect = stuff.GetComponent<RectTransform>();

        rect.SetParent(m_StageStuffsParents);

        if (resetPosition)
        {
            rect.position = m_StageImage.GetComponent<RectTransform>().position;
        }
    }

    public void Clear()
    {
        m_StageImage.texture = null;

        notingText.gameObject.SetActive(true);

        for (int i = 0; i < m_StageStuffsParents.childCount; i++)
        {
            Destroy(m_StageStuffsParents.GetChild(i).gameObject);
        }
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        Stuff.SelectedStuff = null;
    }
}
