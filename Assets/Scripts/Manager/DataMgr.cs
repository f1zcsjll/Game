using System;
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
        /// 保存玩家数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        public void SavePrefsData(string name,object data)
        {
            PlayerPrefs.SetString(name,(string)data.ToString());
        }

        /// <summary>
        /// 获取玩家数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object GetPrefsData(string name,string defaultvalue="")
        {
            return PlayerPrefs.GetString(name,defaultvalue);
        }

        public void SaveAllPrefsData()
        {
            PlayerPrefs.Save();
        }
       
        /// <summary>
        /// 清除玩家某项数据
        /// </summary>
        /// <param name="name"></param>
        public void CleanPrefsData(string name)
        {
            PlayerPrefs.DeleteKey(name);
        }

        /// <summary>
        /// 清除所有玩家数据
        /// </summary>
        public void CleanAllPrefsData()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
