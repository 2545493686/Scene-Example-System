using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueContainer : Stuff
{
    public Dialogue dialoguePrefab;
    public DialogueContainerButton buttonPrefab;
    public Text countText;

    public float spacing = 1.8f;

    public DialogueModel DialogueModel { get; set; }

    List<DialogueContainerButton> m_Dialogues;

    RectTransform m_ButtonRect;
    Transform m_Content;

    int m_ButtonCount = 0;

    void OnEnable()
    {
        m_Dialogues = new List<DialogueContainerButton>();

        m_Content = transform.Find("Content");

        m_ButtonRect = buttonPrefab.GetComponent<RectTransform>();
    }

    protected override void Update()
    {
        base.Update();
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
            dialogue.SetText(content);
            //Vector2 size = dialogue.GetComponent<RectTransform>().sizeDelta;
            dialogue.transform.position = new Vector3
            {
                x = transform.position.x - 140,
                y = transform.position.y + 50 + (100 * (bottonClone.Index)),
            };
            dialogue.transform.parent = transform;
            //StageMaster.Instance.Add(dialogue, false);
        });

        m_Dialogues.Add(bottonClone);

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

        Destroy(m_Dialogues[index].gameObject);
        m_Dialogues.RemoveAt(index);

        foreach (var item in m_Dialogues)
        {
            Debug.Log(item.Index);
        }

        for (int i = index; i < m_Dialogues.Count; i++)
        {
            m_Dialogues[i].GetComponent<RectTransform>().anchoredPosition += new Vector2
            {
                y = m_ButtonRect.rect.height + spacing
            };

            m_Dialogues[i].Index = i;
        }

        countText.text = (--m_ButtonCount).ToString();
    }
}
