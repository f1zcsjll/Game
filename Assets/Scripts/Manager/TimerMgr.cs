using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;

namespace Manager
{
    public class TimerMgr
    {
        private static TimerMgr _instance;
        private Dictionary<string, Timer> TimerList = new Dictionary<string, Timer>();
        private GameObject TimerRoot;
        private TimerMgr()
        {
            TimerRoot = new GameObject("TimerRoot");
            UnityEngine.Object.DontDestroyOnLoad(TimerRoot);
        }
        public static TimerMgr GetInstance()
        {
            if (_instance == null)
                _instance = new TimerMgr();
            return _instance;
        }

        /// <summary>
        /// 添加定时器
        /// </summary>
        /// <param name="delaytime">延迟执行时间</param>
        /// <param name="interval">重复间隔时间，为0不重复</param>
        /// <param name="times">重复执行次数，为0无限执行</param>
        /// <param name="action">执行函数</param>
        public string AddTimer(MVC.Notifier.StandardDelegate action, float delaytime=0, float interval = 0, int times = 0)
        {
            string key = Guid.NewGuid().ToString();
            GameObject timer = new GameObject("Timer" + key);
            timer.transform.parent = TimerRoot.transform;
            Timer newtimer = timer.AddComponent<Timer>();
            newtimer.Init(action, delaytime, interval, times);
            if(newtimer!=null)
            {
                TimerList.Add(key, newtimer);
                return key;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 删除定时器
        /// </summary>
        /// <param name="key"></param>
        public void RemoveTimer(string key)
        {
            if(TimerList.ContainsKey(key))
            {
                TimerList[key].StopTimer();
                TimerList.Remove(key);
            }
        }
    }
}
