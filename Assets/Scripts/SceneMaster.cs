using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RawImage))]
public class SceneMaster : MonoBehaviour, IPointerDownHandler
{
    public string configFolderName = "Scene";
    public Array arrayPrefab;
    public RectTransform stageContent;
    public Text notingText;
    public Text title;

    public static SceneMaster Instance { get; private set; }
    public static Vector3 Point => Instance.transform.position;

    Transform m_StageStuffsParents;
    RawImage m_StageImage;
    SceneData m_StageData = new SceneData();

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

    public void SaveStage()
    {
        m_StageData.sceneMaster = this;
    }

    public void SetStage(string fileName)
    {
        notingText.gameObject.SetActive(false);
        m_StageImage.texture = (Texture)ConfigManager.GetConfig(configFolderName, fileName);
        title.text = ConfigManager.GetRealName(fileName);

        m_StageData.sceneFileName = fileName;
    }

    public void Add(Stuff stuff, bool resetPosition = false)
    {
        RectTransform rect = stuff.GetComponent<RectTransform>();

        rect.SetParent(m_StageStuffsParents);

        if (resetPosition)
        {
            rect.position = m_StageImage.transform.position;
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

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        Stuff.SelectedStuff = null;
    }

    struct SceneData
    {
        public string sceneFileName;
        public SceneMaster sceneMaster;
    }
}
