using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SidebarMaster : MonoBehaviour
{
    public Dropdown dropdown;
    public string defaultSidebarName;

    public Sidebar[] sidebarContents;

    Dictionary<string, GameObject> m_NamesScrollRectsPairs;
    Dictionary<int, string> m_OptionIdsNamesPairs;

    GameObject m_NowSidebar;

    private void Start()
    {
        InitializeNamesScrollRectsPairs();

        SetDropdownOptions();

        SetSidebar(defaultSidebarName);

        dropdown.onValueChanged.AddListener((a) => SetSidebar(GetSidebarName(a)));
    }

    private void InitializeNamesScrollRectsPairs()
    {
        m_NamesScrollRectsPairs = new Dictionary<string, GameObject>();
        foreach (var item in sidebarContents)
            m_NamesScrollRectsPairs.Add(item.name, item.scrollRect.gameObject);
    }

    private void SetDropdownOptions()
    {
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        m_OptionIdsNamesPairs = new Dictionary<int, string>();

        for (int i = 0; i < sidebarContents.Length; i++)
        {
            options.Add(new Dropdown.OptionData(sidebarContents[i].optionName));
            m_OptionIdsNamesPairs.Add(i, sidebarContents[i].name);
        }

        dropdown.options = options;
    }

    public void SetSidebar(string name)
    {
        if (m_NamesScrollRectsPairs.TryGetValue(name, out GameObject @object))
        {
            if (m_NowSidebar)
                m_NowSidebar.SetActive(false);

            @object.SetActive(true);
            m_NowSidebar = @object;
        }
        else
        {
            Debug.Log("Sidebar：没有" + name + "的配置！");
        }
    }

    private string GetSidebarName(int optionId)
    {
        return m_OptionIdsNamesPairs[optionId];
    }

    [System.Serializable]
    public struct Sidebar
    {
        public string name;
        public string optionName;
        public ScrollRect scrollRect;
    }
}
