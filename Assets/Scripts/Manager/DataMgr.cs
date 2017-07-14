﻿using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class DataMgr
    {       
        private static DataMgr _instance;

        /// <summary>
        /// 数据管理器
        /// </summary>
        /// <returns></returns>
        public static DataMgr GetInstance()
        {
            if (_instance == null)
                _instance = new DataMgr();
            return _instance;
        }


        /// <summary>
        /// 读取Localization
        /// </summary>
        /// <param name="LanguageList"></param>
        /// <param name="DicLocalization"></param>
        public static void ReadLocalization(string[] LanguageList,Dictionary<string, string[]> DicLocalization)
        {

        }
    }
}
