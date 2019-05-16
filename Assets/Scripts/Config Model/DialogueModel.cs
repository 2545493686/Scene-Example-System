using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class DialogueModel : ConfigModelBase<string>
{
    protected override string ConfigFolderName => "Dialogue";

    protected override Dictionary<string, string> Datas => datas;
    Dictionary<string, string> datas;

    private void Awake()
    {
        Initialize();
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
