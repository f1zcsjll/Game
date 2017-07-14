using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Text;

public class AddLocalization : EditorWindow {

    static private Dictionary<string, string[]> DicLocalization = new Dictionary<string, string[]>();
    static private int LanguageType = 1;
    static private string[] LanguageList = new string[0];

	[MenuItem("Tools/添加Localization")]
    static void OpenWinAddLoc()
    {

    }
}
