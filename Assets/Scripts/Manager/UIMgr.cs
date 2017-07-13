using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Define;
using Base;

namespace Manager
{
    public class UIMgr
    {
        private static UIMgr _instance;
        private GameObject EventSystem;
        private GameObject UIRoot;
        private Dictionary<UIType, BaseWindow> WinList = new Dictionary<UIType, BaseWindow>();
        private UIType UpUI;
        private UIMgr()
        {            
            UIRoot = new GameObject("UIRoot", typeof(RectTransform),typeof(Canvas),typeof(CanvasScaler),typeof(GraphicRaycaster));
            UIRoot.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            UIRoot.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            UIRoot.GetComponent<CanvasScaler>().referenceResolution = new Vector2(Screen.width, Screen.height);
            UIRoot.GetComponent<CanvasScaler>().screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
            Object.DontDestroyOnLoad(UIRoot);
            EventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
            Object.DontDestroyOnLoad(EventSystem);
        }
        public static UIMgr GetInstance()
        {
            if (_instance == null)
                _instance = new UIMgr();
            return _instance;
        }

        public void ShowWin(UIType type)
        {
            if (!HasWinOpen(type))
            {
                AssetMgr.GetInstance().LoadAsset(PathMgr.GetInstance().GetPath(type), true, true, () =>
                {
                    var win=GameObject.Instantiate(AssetMgr.GetInstance().GetAsset(), UIRoot.transform);
                    WinList.Add(type, ((GameObject)win).GetComponent<BaseWindow>());
                    UpUI = type;
                });
            }
        }

        public void CloseWin(UIType type)
        {
            if (HasWinOpen(type))
            {
                GameObject.Destroy(WinList[type].gameObject);
                WinList.Remove(type);
            }
        }

        public bool HasWinOpen(UIType type)
        {
            return WinList.ContainsKey(type);
        }


    }
}