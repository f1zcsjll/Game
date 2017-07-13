using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class SceneMgr
    {
        public Camera SceneCamera;

        private static SceneMgr _instance;
        private SceneMgr()
        {
            GameObject go = new GameObject("SceneCamera", typeof(Camera), typeof(GUILayer), typeof(FlareLayer));
            SceneCamera = go.GetComponent<Camera>();
            Object.DontDestroyOnLoad(SceneCamera.gameObject);
        }
        public static SceneMgr GetInstance()
        {
            if (_instance == null)
                _instance = new SceneMgr();
            return _instance;
        }
    }
}