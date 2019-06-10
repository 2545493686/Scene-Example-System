using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using System.Text;

[RequireComponent(typeof(RawImage))]
public class SceneMaster : MonoBehaviour, IPointerDownHandler
{
    public string configFolderName = "Scene";
    public RectTransform stageContent;
    public Text notingText;
    public Text title;

    public FileDialog.FilterData dataFilter = new FileDialog.FilterData
    {
        filter = "*.json",
        tag = "场景配置文件(*.json)"
    };

    public static SceneMaster Instance { get; private set; }
    public static Vector3 Point => Instance.transform.position;

    Transform m_StuffsParents;
    RawImage m_StageImage;
    SceneData m_StageData = new SceneData();

    float clickTime;

    const float c_MaxClickTime = 0.2f;

    private void Start()
    {
        Debug.Log(JsonUtility.ToJson(new StuffConfig { stuffFactory = StuffFactory.Instance }));

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
        m_StageData.stuffConfigs = GetStuffConfigs();

        if (FileDialog.TrySave("保存场景", out string savePath, dataFilter))
        {
            File.WriteAllText(savePath, JsonUtility.ToJson(m_StageData), Encoding.GetEncoding("gb2312"));
        }
    }

    public void LoadStage()
    {
        if (FileDialog.TryOpen("加载场景", out string loadPath, dataFilter))
        {
            var json = File.ReadAllText(loadPath, Encoding.GetEncoding("gb2312"));
            m_StageData = JsonUtility.FromJson<SceneData>(json);

            Clear();

            SetStage(m_StageData.sceneFileName);
            foreach (var item in m_StageData.stuffConfigs)
            {
                Add(JsonUtility.FromJson<StuffConfig>(item).Instantiate());
            }
        }
    }

    private string[] GetStuffConfigs()
    {
        List<string> stuffConfigs = new List<string>();
        foreach (var item in m_StuffsParents.GetComponentsInChildren<Stuff>())
        {
            stuffConfigs.Add(item.ToConfigJson());
        }

        return stuffConfigs.ToArray();
    }

    public void SetStage(string fileName)
    {
        if (fileName == null || fileName == string.Empty)
        {
            return;
        }
        notingText.gameObject.SetActive(false);
        m_StageImage.texture = (Texture)ConfigManager.GetConfig(configFolderName, fileName);
        title.text = ConfigManager.GetRealName(fileName);

        m_StageData.sceneFileName = fileName;
    }

    public void Add(Stuff stuff, bool resetPosition = false)
    {
        RectTransform rect = stuff.GetComponent<RectTransform>();

        rect.SetParent(m_StuffsParents);

        if (resetPosition)
        {
            rect.position = m_StageImage.transform.position;
        }
    }

    public void Clear()
    {
        m_StageImage.texture = null;

        notingText.gameObject.SetActive(true);

        for (int i = 0; i < m_StuffsParents.childCount; i++)
        {
            Destroy(m_StuffsParents.GetChild(i).gameObject);
        }
    }


    private void OnDoubleClick()
    {
        Add(ArrayFactory.Instance.Instantiate(new ArrayFactory.InstantiateData
        {
            worldPoint = Input.mousePosition,
            endPoint = Input.mousePosition,
            isMoving = true,
        }));
    }

    private void InitializeStuffs()
    {
        GameObject stuffs = new GameObject("Stuffs");
        stuffs.transform.SetParent(transform);
        m_StuffsParents = stuffs.transform;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        Stuff.SelectedStuff = null;
    }

    struct SceneData
    {
        public string sceneFileName;
        public string[] stuffConfigs;
    }
}
