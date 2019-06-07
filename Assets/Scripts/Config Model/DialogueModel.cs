using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine.Events;

public class DialogueModel : ConfigModelBase<string>
{
    public string folderName = "Dialogue";
    protected override string FolderName => folderName;

    protected override Dictionary<string, string> Datas => datas;
    Dictionary<string, string> datas;

    protected override void Initialize(UnityEvent onInitialized)
    {
        Initialize();
        onInitialized.Invoke();
    }

    protected void Initialize()
    {
        datas = new Dictionary<string, string>();
        var files = GetAllFiles("*.txt");

        foreach (var item in files)
        {
            datas.Add
            (
                key: item.Name.Substring(0, item.Name.Length - 4),
                value: File.ReadAllText(item.FullName, Encoding.GetEncoding("gb2312"))
            );
        }
    }
}
