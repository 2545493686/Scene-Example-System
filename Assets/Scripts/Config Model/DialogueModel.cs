using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class DialogueModel : ConfigModelBase<string>
{
    protected override string ConfigFolderName => "Dialogue";

    //private string GetStuffConfigPath(StuffImageData stuffData)
    //{
    //    return ConfigPath + "\\" + stuffData.name + ".txt";
    //}

    protected override InitializeData[] Initialize()
    {
        //Dictionary<string, string> ret = new Dictionary<string, string>();

        DirectoryInfo root = new DirectoryInfo(ConfigPath);
        FileInfo[] files = root.GetFiles("*.txt");
        InitializeData[] ret = new InitializeData[files.Length];
        for (int i = 0; i < files.Length; i++)
        {
            //ret.Add(item.Name.Substring(0, item.Name.Length - 4), File.ReadAllText(item.FullName, Encoding.GetEncoding("gb2312")));

            FileInfo item = files[i];
            ret[i] = new InitializeData
            {
                title = item.Name.Substring(0, item.Name.Length - 4),
                data = File.ReadAllText(item.FullName, Encoding.GetEncoding("gb2312"))
            };
        }

        return ret;
    }
}
