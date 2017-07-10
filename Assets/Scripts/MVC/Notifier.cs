using System;
using System.Collections;
using System.Collections.Generic;

namespace MVC
{
    public class Notifier
    {
        public delegate void StandardDelegate(params object[] arg);
        private Dictionary<string, StandardDelegate> EventMap = new Dictionary<string, StandardDelegate>();

        /// <summary>
        /// 注册时间
        /// </summary>
        /// <param name="name"></param>
        /// <param name="Del"></param>
        public void AddEventHandler(string name, StandardDelegate Del)
        {
            if (!EventMap.ContainsKey(name))
            {
                EventMap.Add(name, Del);
            }
            else
            {
                EventMap[name] += Del;
            }
        }

        /// <summary>
        /// 移除事件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="Del"></param>
        public void RemoveEventHandler(string name, StandardDelegate Del)
        {
            if (EventMap.ContainsKey(name))
            {
                if (EventMap[name] != null)
                {
                    EventMap[name] -= Del;
                }
            }
        }

        public bool HasEvent(string name)
        {
            return EventMap.ContainsKey(name);
        }

        /// <summary>
        /// 执行事件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="arg"></param>
        public void RaiseEvent(string name, params object[] arg)
        {
            if (EventMap.ContainsKey(name))
            {
                StandardDelegate fun = null;
                if (EventMap.TryGetValue(name, out fun))
                {
                    if (fun != null)
                    {
                        fun(arg);
                    }
                }
            }
        }

        public void RemoveAllEventHandler(string name)
        {
            if (EventMap.ContainsKey(name))
            {
                if (EventMap[name] != null)
                {
                    Delegate[] DelArr = EventMap[name].GetInvocationList();
                    for (int i = 0; i < DelArr.Length; i++)
                    {
                        RemoveEventHandler(name, EventMap[name]);
                    }
                }
            }
        }

        public void CleanAllEvent()
        {
            if (EventMap.Count >= 0)
            {
                EventMap.Clear();
            }
        }
    }
}
