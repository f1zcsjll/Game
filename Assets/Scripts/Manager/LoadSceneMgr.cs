using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class LoadSceneMgr
    {
        private static LoadSceneMgr _instance;
        private static AsyncOperation ao;
 
        /// <summary>
        /// 场景加载管理器
        /// </summary>
        /// <returns></returns>
        public static LoadSceneMgr GetInstance()
        {
            if (_instance == null)
                _instance = new LoadSceneMgr();
            return _instance;
        }

        public static void LoadSence(string name)
        {
            SceneManager.LoadScene("Scenes/WinLoading");
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
