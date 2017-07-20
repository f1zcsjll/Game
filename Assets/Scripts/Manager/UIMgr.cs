using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Define;
using Base;
using System;

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
            UnityEngine.Object.DontDestroyOnLoad(UIRoot);
            EventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
            UnityEngine.Object.DontDestroyOnLoad(EventSystem);
        }
        public static UIMgr GetInstance()
        {
            if (_instance == null)
                _instance = new UIMgr();
            return _instance;
        }

        /// <summary>
        /// 打开界面
        /// </summary>
        /// <param name="type">界面类型</param>
        /// <param name="funtion">完成后回调</param>
        public void ShowWin(UIType type,Action funtion=null)
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
            else
            {
                WinList[type].gameObject.SetActive(true);
            }
            if (funtion != null)
                funtion();
        }

        /// <summary>
        /// 获取已打开的界面的Object
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public GameObject GetWin(UIType type)
        {
            if(HasWinOpen(type))
            {
                return WinList[type].gameObject;
            }
            return null;
        }

        /// <summary>
        /// 关闭界面
        /// </summary>
        /// <param name="type">界面类型</param>
        /// <param name="funtion">完成后回调</param>
        public void CloseWin(UIType type, Action funtion = null)
        {
            if (HasWinOpen(type))
            {
                GameObject.Destroy(WinList[type].gameObject);
                WinList.Remove(type);
                funtion();
            }
        }

        /// <summary>
        /// 某界面是否打开
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool HasWinOpen(UIType type)
        {
            return WinList.ContainsKey(type);
        }

        /// <summary>
        /// 隐藏界面
        /// </summary>
        /// <param name="type">窗口类型</param>
        /// <param name="isHide">是否隐藏</param>
        /// <param name="funtion">完成后回调</param>
        public void HideWin(UIType type, bool isHide, Action funtion = null)
        {
            if(HasWinOpen(type))
            {
                WinList[type].gameObject.SetActive(!isHide);
                funtion();
            }
        }

        /// <summary>
        /// 隐藏所有界面，保留过滤列表里面的界面
        /// </summary>
        /// <param name="isHide"></param>
        public void HideAllWin(bool isHide, List<UIType> filter)
        {
            List<UIType> closelist = new List<UIType>(WinList.Keys);
            var e = closelist.GetEnumerator();
            while (e.MoveNext())
            {
                if (!filter.Contains(e.Current))
                    HideWin(e.Current, true);
            }
            e.Dispose();
            closelist.Clear();
        }

        /// <summary>
        /// 关闭所有界面
        /// </summary>
        public void CloseAllWin()
        {
            List<UIType> closelist = new List<UIType>(WinList.Keys);
            var e = closelist.GetEnumerator();
            while (e.MoveNext())
            {
                CloseWin(e.Current);
            }
            e.Dispose();
            closelist.Clear();
        }

        /// <summary>
        /// 关闭所有界面，保留过滤列表里面的界面
        /// </summary>
        /// <param name="filter"></param>
        public void CloseWinByFilter(List<UIType> filter)
        {
            List<UIType> closelist = new List<UIType>(WinList.Keys);
            var e = closelist.GetEnumerator();
            while (e.MoveNext())
            {
                if(!filter.Contains(e.Current))
                    CloseWin(e.Current);
            }
            e.Dispose();
            closelist.Clear();
        }
    }
}