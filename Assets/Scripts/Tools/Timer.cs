using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public class Timer :MonoBehaviour
    {
        private float DelayTime = 0;
        private float Interval = 0;
        private int Times = 0;
        private MVC.Notifier.StandardDelegate Action;

        /// <summary>
        /// 初始化定时器
        /// </summary>
        /// <param name="delaytime">延迟执行时间</param>
        /// <param name="interval">重复间隔时间，为0不重复</param>
        /// <param name="times">重复执行次数，为0无限执行</param>
        /// <param name="action">执行函数</param>
        public Timer(MVC.Notifier.StandardDelegate action, float delaytime=0, float interval = 0, int times = 0)
        {
            DelayTime = delaytime;
            Interval = interval;
            Times = times;
            Action = action;
            if(Interval==0)
            {
                Invoke(Action.Method.Name, DelayTime);
            }
            else if(Times==0)
            {
                InvokeRepeating(Action.Method.Name, DelayTime, Interval);
            }
            else
            {
                Invoke("TimesAction", DelayTime);
            }
        }

        void TimesAction()
        {
            for(int i=Times-1;i>=0;i--)
            {
                StartCoroutine(Do(i * Interval));
            }
        }

        IEnumerator Do(float waittime)
        {
            yield return new WaitForSeconds(waittime);
            Action();
            CancelInvoke("TimesAction");
        }

        /// <summary>
        /// 停止计时器
        /// </summary>
        public void StopTimer()
        {
            CancelInvoke(Action.Method.Name);
            CancelInvoke("TimesAction");
            StopAllCoroutines();
        }
    }
}
