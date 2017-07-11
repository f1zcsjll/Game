using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class LoadSenceMgr
    {
        private static LoadSenceMgr _instance;
        private static AsyncOperation ao;
 
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

        public static void LoadSence(string name)
        {
            SceneManager.LoadScene("WinLoading");
            ao = null;
            LoadSenceAsync(name);
        }

        static void LoadSenceAsync(string name)
        {
            ao=SceneManager.LoadSceneAsync(name);
        }

        public float GetLoadProgress()
        {
            if (ao != null)
                return ao.progress;
            return 0;
        }
    }
}
