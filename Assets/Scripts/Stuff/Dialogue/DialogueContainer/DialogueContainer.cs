using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct DialogueContent
{
    public string title;
    public string content;
}

public class DialogueContainer : Stuff
{
    public InputField title;
    public Dialogue dialoguePrefab;
    public DialogueContainerButton buttonPrefab;
    public Text countText;

    public float spacing = 1.8f;

    public DialogueModel DialogueModel { get; set; }
    public int DialogueCount => m_Dialogues.Count;

    List<DialogueContainerButton> m_DialogueButtons = new List<DialogueContainerButton>();
    List<DialogueContent> m_DialogueContents = new List<DialogueContent>();
    List<Dialogue> m_Dialogues = new List<Dialogue>();

    RectTransform m_ButtonRect;
    Transform m_Content;

    string m_ContainerTitle;
    int m_ButtonCount = 0;

    void OnEnable()
    {
        m_Content = transform.Find("Content");
        m_ButtonRect = buttonPrefab.GetComponent<RectTransform>();
    }

    protected override void Update()
    {
        base.Update();
    }

    public void RemoveDialogue(Dialogue dialogue)
    {
        var index = m_Dialogues.IndexOf(dialogue);
        for (int i = index + 1; i < m_Dialogues.Count; i++)
        {
            m_Dialogues[i].transform.position -= new Vector3
            {
                y = 100
            };
        }
        m_Dialogues.RemoveAt(index);
    }

    public void SetTitle(string text)
    {
        m_ContainerTitle = text;
        if (title.text != text)
        {
            title.text = text;
        }
    }

    public void SetContent(int index, string text)
    {
        var content = m_DialogueContents[index];
        content.content = text;
        m_DialogueContents[index] = content;
    }

    public void AddDialogue(DialogueContent dialogueContent)
    {
        AddDialogue(dialogueContent.title, dialogueContent.content);
    }

    public void AddDialogue(string title, string content)
    {
        RectTransform.sizeDelta += new Vector2 { y = m_ButtonRect.rect.height + spacing };

        var bottonClone = Instantiate(buttonPrefab, m_Content);

        bottonClone.Index = m_ButtonCount;
        bottonClone.name = title;
        bottonClone.transform.GetComponentInChildren<Text>().text = title;

        bottonClone.Button.onClick.AddListener(() => 
        {
            var dialogue = Instantiate(dialoguePrefab);
            dialogue.SetText(m_DialogueContents[bottonClone.Index].content);
            dialogue.Index = bottonClone.Index;
            dialogue.DialogueContainer = this;
            dialogue.transform.position = new Vector3
            {
                x = transform.position.x - 140,
                y = transform.position.y + 50 + (100 * (DialogueCount)),
            };
            dialogue.transform.parent = transform;

            m_Dialogues.Add(dialogue);
        });

        m_DialogueButtons.Add(bottonClone);
        m_DialogueContents.Add(new DialogueContent
        {
            title = title,
            content = content
        });

        var bottonCloneRect = bottonClone.GetComponent<RectTransform>();
        bottonCloneRect.anchoredPosition = GetCloneButtonPosition();
        bottonCloneRect.gameObject.SetActive(true);

        countText.text = (++m_ButtonCount).ToString();
    }

    private Vector2 GetCloneButtonPosition()
    {
        return m_ButtonRect.anchoredPosition - new Vector2 { y = (m_ButtonRect.rect.height + spacing) * m_ButtonCount  };
    }

    public void DelDialogue(int index)
    {
        RectTransform.sizeDelta -= new Vector2 { y = m_ButtonRect.rect.height + spacing };

        Destroy(m_DialogueButtons[index].gameObject);
        m_DialogueButtons.RemoveAt(index);
        m_DialogueContents.RemoveAt(index);

        foreach (var item in m_DialogueButtons)
        {
            Debug.Log(item.Index);
        }

        for (int i = index; i < m_DialogueButtons.Count; i++)
        {
            m_DialogueButtons[i].GetComponent<RectTransform>().anchoredPosition += new Vector2
            {
                y = m_ButtonRect.rect.height + spacing
            };

            m_DialogueButtons[i].Index = i;
        }

        countText.text = (--m_ButtonCount).ToString();
    }

    protected override string ToInstantiateJson()
    {
        return JsonUtility.ToJson(new DialogueFactory.InstantiateData
        {
            dialogueContents = m_DialogueContents.ToArray(),
            worldPoint = transform.position,
            containerTitle = m_ContainerTitle
        });
    }
}
