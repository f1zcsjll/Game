using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Text;

namespace Tools
{
    public class AddLanguage : EditorWindow
    {
        static private Dictionary<string, string> DicLocalization = new Dictionary<string, string>();
        static private List<string> language = new List<string>();
        static private string addlanguage = "";
        static private string path = Localization.Path;

        [MenuItem("Tools/添加语言")]
        static void OpenWinAddLoc()
        {
            ReadLocalization();
            GetWindowWithRect(typeof(AddLanguage), new Rect(0, 0, 400, 100), true, "添加语言");
        }

        void OnGUI()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("语言名称：", GUILayout.Width(60));
            addlanguage = EditorGUILayout.TextField(addlanguage);
            GUILayout.EndHorizontal();            
            GUILayout.Space(5);
            if (GUILayout.Button("添加"))
            {
                if (string.IsNullOrEmpty(addlanguage))
                {
                    EditorUtility.DisplayDialog("", "语言不能为空", "ok");
                }
                else if (language.Contains(addlanguage))
                {
                    EditorUtility.DisplayDialog("", "该语言已存在", "ok");
                }
                else
                {
                    ChangeLocalization();
                    SaveLocalization();
                }
            }
        }

        /// <summary>
        /// 读取Localization
        /// </summary>
        static void ReadLocalization()
        {
            DicLocalization.Clear();
            if (File.Exists(path))
            {
                StreamReader sr = new StreamReader(path, Encoding.UTF8);
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    if (language.Count == 0)
                    {
                        language = new List<string>(line.Split(','));
                    }
                    string[] data = line.Split(new[] { ',' }, StringSplitOptions.None);
                    DicLocalization.Add(data[0], line);
                }
                sr.Close();
            }
        }

        /// <summary>
        /// 修改Localization
        /// </summary>
        static void ChangeLocalization()
        {
            bool isfirst = true;
            var e = new List<string>(DicLocalization.Keys);
            for (int i = 0; i < e.Count;i++)
            {
                if (isfirst)
                {
                    DicLocalization[e[i]] += "," + addlanguage;
                    language.Add(addlanguage);
                    isfirst = false;
                }
                else
                {
                    DicLocalization[e[i]] += ",";
                }
            }
        }

        /// <summary>
        /// 保存Localization
        /// </summary>
        static void SaveLocalization()
        {
            StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8);
            var e = DicLocalization.GetEnumerator();
            while (e.MoveNext())
            {
                sw.WriteLine(e.Current.Value);
            }
            e.Dispose();
            sw.Close();
            EditorUtility.DisplayDialog("", "添加成功", "ok");
        }
    }
}