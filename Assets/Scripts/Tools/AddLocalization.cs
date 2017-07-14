using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Text;

public class AddLocalization : EditorWindow {

    static private Dictionary<string, string[]> DicLocalization = new Dictionary<string, string[]>();
    static private List<string> language = new List<string>();
    static private string[] showstr = new string[0];
    static private readonly string path = Application.dataPath + "/Resources/" + Manager.PathMgr.GetInstance().GetPath(Define.DataType.Localization)+".txt";

	[MenuItem("Tools/添加Localization")]
    static void OpenWinAddLoc()
    {
        ReadLocalization();
        GetWindowWithRect(typeof(AddLocalization), new Rect(0, 0, 400, language.Count * 60 + 80), true, "添加Localization");
    }

    void OnGUI()
    {
        for(int i=0;i<language.Count;i++)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(language[i], GUILayout.Width(60));
            showstr[i] = EditorGUILayout.TextField(showstr[i]);
            GUILayout.EndHorizontal();
        }
        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("修改"))
        {
            if (string.IsNullOrEmpty(showstr[0]))
            {
                EditorUtility.DisplayDialog("Error", "Key不能为空", "ok");
            }
            else
            {
                ChangeLocalization();
            }
        }
        if(GUILayout.Button("保存"))
        {
            SaveLocalization();
        }
        GUILayout.EndHorizontal();
    }

    /// <summary>
    /// 读取Localization
    /// </summary>
    public static void ReadLocalization()
    {
        DicLocalization.Clear();
        if (File.Exists(path))
        {
            StreamReader sr = new StreamReader(path, Encoding.UTF8);
            string line="";
            while((line=sr.ReadLine())!=null)
            {
                if(language.Count==0)
                {
                    language = new List<string>(line.Split(','));
                    showstr = new string[language.Count];
                    for(int i=0;i<language.Count;i++)
                    {
                        showstr[i] = string.Empty;
                    }
                }
                char[] split = { ',' };
                string[] data=line.Split(split,StringSplitOptions.None);
                DicLocalization.Add(data[0], data);
            }
            sr.Close();
        }
    }

    /// <summary>
    /// 修改Localization
    /// </summary>
    public static void ChangeLocalization()
    {
        if(DicLocalization.ContainsKey(showstr[0]))
        {
            for(int i=1;i<showstr.Length;i++)
            {
                if(!string.IsNullOrEmpty(showstr[i]))
                {
                    DicLocalization[showstr[0]][i]=showstr[i];
                }
            }
        }
        else
        {
            DicLocalization.Add(showstr[0], showstr);
        }
        EditorUtility.DisplayDialog("", "修改成功", "ok");
    }

    /// <summary>
    /// 保存Localization
    /// </summary>
    public static void SaveLocalization()
    {
        StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8);
        string line = "";
        int num=0;
        var e=DicLocalization.GetEnumerator();
        while(e.MoveNext())
        {
            if(num==0)
            {
                num = e.Current.Value.Length;
            }
            for(int i=0;i<num;i++)
            {
                if(!string.IsNullOrEmpty(e.Current.Value[i]))
                {
                    if(i==0)
                    {
                        line += e.Current.Value[i];
                    }
                    else
                    {
                        line += ("," + e.Current.Value[i]);
                    }
                }
                else
                {
                    line += ",";
                }
            }
            sw.WriteLine(line);
            line = "";
        }
        e.Dispose();
        sw.Close();
        EditorUtility.DisplayDialog("", "保存成功", "ok");
    }
}
