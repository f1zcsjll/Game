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
        private Dictionary<int, Timer> TimerList = new Dictionary<int, Timer>();
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
        public int AddTimer(MVC.Notifier.StandardDelegate action, float delaytime=0, float interval = 0, int times = 0)
        {
            Timer newtimer = new Timer(action, delaytime, interval, times);
            if(newtimer!=null)
            {
                int key=newtimer.GetHashCode();
                TimerList.Add(key, newtimer);
                return key;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 删除定时器
        /// </summary>
        /// <param name="key"></param>
        public void RemoveTimer(int key)
        {
            if(TimerList.ContainsKey(key))
            {
                TimerList[key].StopTimer();
                TimerList.Remove(key);
            }
        }
    }
}
