using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

public static class Localization 
{
    static private Dictionary<string, string[]> DicLocalization = new Dictionary<string, string[]>();
    static private int LanguageType = 1;
    static private string[] LanguageList = new string[0];
    public static readonly string Path = Application.dataPath + "/Resources/" + Manager.PathMgr.GetInstance().GetPath(Define.DataType.Localization) + ".txt";

    private static void Init()
    {
        if(DicLocalization.Count==0)
        {
            AssetMgr.GetInstance().LoadAsset(PathMgr.GetInstance().GetPath(Define.DataType.Localization),false,false,()=>{
                string[] loc = AssetMgr.GetInstance().GetAsset().ToString().Split(new[]{'\n'},System.StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < loc.Length;i++)
                {
                    loc[i] = loc[i].Replace("\r", "");
                }
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
            {                
                if (DicLocalization[key][LanguageType].Contains("[b-]"))
                {
                    DicLocalization[key][LanguageType] = DicLocalization[key][LanguageType].Replace("[b-]", "</b>");
                    DicLocalization[key][LanguageType] = DicLocalization[key][LanguageType].Replace("[b]", "<b>");
                }
                if (DicLocalization[key][LanguageType].Contains("[i-]"))
                {
                    DicLocalization[key][LanguageType] = DicLocalization[key][LanguageType].Replace("[i-]", "</i>");
                    DicLocalization[key][LanguageType] = DicLocalization[key][LanguageType].Replace("[i]", "<i>");
                }
                if (DicLocalization[key][LanguageType].Contains("[c-]"))
                {
                    DicLocalization[key][LanguageType] = DicLocalization[key][LanguageType].Replace("[c-", "</color");
                    DicLocalization[key][LanguageType] = DicLocalization[key][LanguageType].Replace("[c", "<Color=#");
                    DicLocalization[key][LanguageType] = DicLocalization[key][LanguageType].Replace("]", ">");
                }
                if (DicLocalization[key][LanguageType].Contains("[s-]") || DicLocalization[key][LanguageType].Contains("[s->"))
                {
                    DicLocalization[key][LanguageType] = DicLocalization[key][LanguageType].Replace("[s-", "</size");
                    DicLocalization[key][LanguageType] = DicLocalization[key][LanguageType].Replace("[s", "<size=");
                    DicLocalization[key][LanguageType] = DicLocalization[key][LanguageType].Replace("]", ">");
                }
                return DicLocalization[key][LanguageType].Replace("\\n", "\n");
            }
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
