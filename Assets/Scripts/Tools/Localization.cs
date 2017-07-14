using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

public static class Localization 
{
    static private Dictionary<string, string[]> DicLocalization = new Dictionary<string, string[]>();
    static private int LanguageType = 1;
    static private string[] LanguageList = new string[0];

    private static void Init()
    {
        if(DicLocalization.Count==0)
        {
            AssetMgr.GetInstance().LoadAsset(PathMgr.GetInstance().GetPath(Define.DataType.Localization),false,false,()=>{
                string[] loc = AssetMgr.GetInstance().GetAsset().ToString().Split('\n');
                LanguageList = loc[0].Split(',');
                for(int i=1;i<loc.Length;i++)
                {
                    string[] data=loc[i].Split(',');
                    DicLocalization.Add(data[0], data);
                }
            });
        }
    }

    /// <summary>
    /// 根据Key获取文本
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string Get(string key)
    {
        Init();
        if(DicLocalization.ContainsKey(key))
        {
            if (!string.IsNullOrEmpty(DicLocalization[key][LanguageType]))
                return DicLocalization[key][LanguageType];
            else
                return key;
        }
        else
        {
            return key;
        }
    }

    /// <summary>
    /// 设定当前语言种类
    /// </summary>
    /// <param name="language"></param>
    public static void SetLanguage(string language)
    {
        Init();
        for(int i=1;i<LanguageList.Length;i++)
        {
            if(LanguageList[i]==language)
            {
                LanguageType = i;
                return;
            }
        }
        LanguageType = 1;
    }
}
