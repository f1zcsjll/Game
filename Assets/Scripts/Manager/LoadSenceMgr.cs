using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class LoadSenceMgr
    {
        private static LoadSenceMgr _instance;
 
        /// <summary>
        /// 场景加载管理器
        /// </summary>
        /// <returns></returns>
        public static LoadSenceMgr GetInstance()
        {
            if (_instance == null)
                _instance = new LoadSenceMgr();
            return _instance;
        }
    }
}
