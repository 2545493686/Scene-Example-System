﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueModel))]
public class EtiquettesSidebar : SidebarBase<DialogueImage>
{
    public Dialogue dialoguePrefabs;
    public DialogueImage dialogueImagePrefabs;

    public override DialogueImage StuffImagePrefabs => dialogueImagePrefabs;

    private void Start()
    {
        DialogueModel dialogueModel = GetComponent<DialogueModel>();

        string[] dialogueTitles = dialogueModel.GetAllDialogueTitles();

        StuffImageData[] stuffDatas = new StuffImageData[dialogueTitles.Length];

        SetContentParents(dialogueTitles.Length);

        int i = 0;
        foreach (var item in dialogueTitles)
        {
            stuffDatas[i++] = new StuffImageData { name = item };
        }

        DialogueImage[] dialogueImages = CreateStuffImages(stuffDatas);

        foreach (DialogueImage item in dialogueImages)
        {
            item.Content = dialogueModel.GetDialogue(item.Data.name);
            item.DialoguePrefabs = dialoguePrefabs;
        }
    }

}
