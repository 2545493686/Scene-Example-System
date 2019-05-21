﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueContainerButton : MonoBehaviour, IPointerDownHandler, IPointerExitHandler
{
    public DialogueContainer dialogueContainer;

    public int Index { get; set; }

    bool m_Selected = false;

    private void Update()
    {
        if (m_Selected && (Input.GetKey(KeyCode.Delete) || Input.GetKey(KeyCode.Mouse1)))
        {
            dialogueContainer.DelDialogue(Index);
        }
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        m_Selected = true;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        m_Selected = false;
    }
}
