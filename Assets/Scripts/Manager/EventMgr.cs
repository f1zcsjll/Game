using System.Collections;
using System.Collections.Generic;
using MVC;

namespace Manager
{
    public class EventMgr:Notifier
    {
        private static EventMgr _instance;

        /// <summary>
        /// 全局事件管理器
        /// </summary>
        /// <returns></returns>
        public static EventMgr GetInstance()
        {
            if (_instance == null)
                _instance = new EventMgr();
            return _instance;
        }
    }
}
