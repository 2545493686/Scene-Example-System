using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueModel))]
public class DialogueSidebar : SidebarBase<DialogueImage>
{

    public DialogueContainer dialoguePrefabs;
    public DialogueImage dialogueImagePrefabs;

    public override DialogueImage StuffImagePrefabs => dialogueImagePrefabs;

    private void Start()
    {
        string[] dialogueTitles = ConfigManager.GetAllTitles(configFolderName);

        ImageGridData[] stuffDatas = new ImageGridData[dialogueTitles.Length];

        //SetContentParentHeight(dialogueTitles.Length);

        int i = 0;
        foreach (var item in dialogueTitles)
        {
            stuffDatas[i++] = new ImageGridData { title = item };
        }

        DialogueImage[] dialogueImages = CreateStuffImages(stuffDatas);

        foreach (DialogueImage item in dialogueImages)
        {
            item.Content = (string)ConfigManager.GetConfig(configFolderName, item.Data.title);
            item.DialoguePrefabs = dialoguePrefabs;
        }
    }
}
