using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Text;

namespace Tools.Editor
{
    public class AddConfigData : EditorWindow
    {

        [MenuItem("Tools/选择CSV文件添加数据CS")]
        static void AddDataCS()
        {
            string[] typelist;
            string path = Application.dataPath.Replace("/Assets","")+"/"+AssetDatabase.GetAssetPath(Selection.activeInstanceID);
            if (path.LastIndexOf(".csv") >= 0 || path.LastIndexOf(".CSV") >= 0)
            {
                StreamReader sr = new StreamReader(path, Encoding.UTF8);
                typelist = sr.ReadLine().Split(',');
                sr.Close();
                if(typelist.Length>0)
                {
                    CreateDataCS(Application.dataPath + "/Scripts/Config/Data/" + "C" + Selection.objects[0].name + ".cs", typelist, Selection.objects[0].name);
                }
            }
            else
            {
                Debug.LogError("Select File Error!");
            }
        }

        static void CreateDataCS(string path,string[] typelist,string name)
        {
            StreamWriter sw = new StreamWriter(path, false ,Encoding.UTF8);
            sw.WriteLine("using Base;");
            sw.WriteLine();
            sw.WriteLine("namespace Config.Data {");
            sw.WriteLine("\tpublic class C"+name+" :BaseData {");
            sw.WriteLine("\t\tpublic enum Type{");
            for(int i=0;i<typelist.Length;i++)
            {
                sw.WriteLine("\t\t\t"+typelist[i] + ",");
            }
            sw.WriteLine("\t\t};");
            sw.WriteLine();
            sw.WriteLine("\t\tpublic override BaseData ReadData(string data) {");
            sw.WriteLine("\t\t\tstring[] temp = data.Split(',');");
            sw.WriteLine("\t\t\tfor (int i = 0; i < temp.Length;i++) {");
            sw.WriteLine("\t\t\t\tDataList.Add((Type)i, temp[i]); }");
            sw.WriteLine("\t\t\treturn this;");
            sw.WriteLine("}}}");
            sw.Close();
            EditorUtility.DisplayDialog("", "创建完成", "ok");
        }
    }
}