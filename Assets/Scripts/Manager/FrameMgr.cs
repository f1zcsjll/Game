using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class FrameMgr
    {
        private static FrameMgr _instance;
        private FrameMgr()
        {

        }
        /// <summary>
        /// 全局帧管理器
        /// </summary>
        /// <returns></returns>
        public static FrameMgr GetInstance()
        {
            if (_instance == null)
                _instance = new FrameMgr();
            return _instance;
        }

        Action OnUpdate;
        Action OnFixedUpdate;
        Action OnLateUpdate;

        #region 注册事件
        public void RegisterUpdate(Action Event)
        {
            OnUpdate += Event;
        }

        public void RegisterFixedUpdate(Action Event)
        {
            OnFixedUpdate += Event;
        }

        public void RegisterLateUpdate(Action Event)
        {
            OnLateUpdate += Event;
        }
        #endregion

        #region 注销事件
        public void UnRegisterUpdate(Action Event)
        {
            OnUpdate -= Event;
        }

        public void UnRegisterFixedUpdate(Action Event)
        {
            OnFixedUpdate -= Event;
        }

        public void UnRegisterLateUpdate(Action Event)
        {
            OnLateUpdate -= Event;
        }
        #endregion

        #region 执行事件
        public void DoUpdate()
        {
            if (OnUpdate != null)
                OnUpdate();
        }

        public void DoFixedUpdate()
        {
            if (OnFixedUpdate != null)
                OnFixedUpdate();
        }

        public void DoLateUpdate()
        {
            if (OnLateUpdate != null)
                OnLateUpdate();
        }
        #endregion
    }
}
