using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueModel))]
public class EtiquettesSidebar : SidebarBase
{
    private void Start()
    {
        DialogueModel dialogueModel = GetComponent<DialogueModel>();

        StuffData[] stuffDatas = new StuffData[dialogueModel.GetAllDialogueTitles().Length];



        foreach (var item in dialogueModel.GetAllDialogueTitles())
        {

        }
    }
}
