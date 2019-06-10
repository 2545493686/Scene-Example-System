using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class FileDialog
{
    [Serializable]
    public struct FilterData
    {
        public string tag;
        public string filter;

        public string GetFilterEnds()
        {
            return filter.Substring(filter.IndexOf('.'), filter.Length - filter.IndexOf('.'));
        }
    }

    public static bool TryOpen(string dialogTitle, out string filePath, params FilterData[] filterDatas)
    {
        filePath = Open(dialogTitle, filterDatas);

        if (filePath == null && filePath == string.Empty)
            return false;
        else
            return true;
    }

    public static string Open(string dialogTitle, params FilterData[] filterDatas)
    {
        OpenFileName openFileName = GetOpenFileName(dialogTitle, filterDatas);

        if (LocalDialog.GetOpenFileName(openFileName))
        {
            return openFileName.file;
        }
        return null;
    }

    public static bool TrySave(string dialogTitle, out string filePath, params FilterData[] filterDatas)
    {
        filePath = Save(dialogTitle, filterDatas);

        if (filePath == null && filePath == string.Empty)
            return false;
        else
            return true;
    }

    public static string Save(string dialogTitle, params FilterData[] filterDatas)
    {
        OpenFileName openFileName = GetOpenFileName(dialogTitle, filterDatas);
        
        if (LocalDialog.GetSaveFileName(openFileName))
        {
            string ret = openFileName.file;
            if (openFileName.filterIndex != 0)
            {
                if (!ret.EndsWith(filterDatas[openFileName.filterIndex - 1].GetFilterEnds()))
                {
                    ret += filterDatas[openFileName.filterIndex - 1].GetFilterEnds();
                }
            }
            return ret;
        }
        return null;
    }

    private static OpenFileName GetOpenFileName(string dialogTitle, FilterData[] filterDatas)
    {
        OpenFileName openFileName = new OpenFileName();
        openFileName.structSize = Marshal.SizeOf(openFileName);
        openFileName.filter = "";
        foreach (FilterData filter in filterDatas)
        {
            openFileName.filter += $"{filter.tag}\0{filter.filter}\0";
        }
        openFileName.filter += "\0";
        openFileName.file = new string(new char[256]);
        openFileName.maxFile = openFileName.file.Length;
        openFileName.fileTitle = new string(new char[64]);
        openFileName.maxFileTitle = openFileName.fileTitle.Length;
        openFileName.initialDir = Application.streamingAssetsPath.Replace('/', '\\');//默认路径
        openFileName.title = dialogTitle;
        openFileName.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000008;
        return openFileName;
    }
}

public class LocalDialog
{
    //链接指定系统函数       打开文件对话框
    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool GetOpenFileName([In, Out] OpenFileName ofn);
    public static bool GetOFN([In, Out] OpenFileName ofn)
    {
        return GetOpenFileName(ofn);
    }

    //链接指定系统函数        另存为对话框
    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool GetSaveFileName([In, Out] OpenFileName ofn);
    public static bool GetSFN([In, Out] OpenFileName ofn)
    {
        return GetSaveFileName(ofn);
    }
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public class OpenFileName
{
    public int structSize = 0;
    public IntPtr dlgOwner = IntPtr.Zero;
    public IntPtr instance = IntPtr.Zero;
    public String filter = null;
    public String customFilter = null;
    public int maxCustFilter = 0;
    public int filterIndex = 0;
    public String file = null;
    public int maxFile = 0;
    public String fileTitle = null;
    public int maxFileTitle = 0;
    public String initialDir = null;
    public String title = null;
    public int flags = 0;
    public short fileOffset = 0;
    public short fileExtension = 0;
    public String defExt = null;
    public IntPtr custData = IntPtr.Zero;
    public IntPtr hook = IntPtr.Zero;
    public String templateName = null;
    public IntPtr reservedPtr = IntPtr.Zero;
    public int reservedInt = 0;
    public int flagsEx = 0;
}