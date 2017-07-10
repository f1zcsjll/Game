using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public class View : MonoBehaviour
    {
        protected Model BindModel;
        Dictionary<string, Notifier.StandardDelegate> FunList = new Dictionary<string, Notifier.StandardDelegate>();

        /// <summary>
        /// 绑定模型
        /// </summary>
        /// <param name="model"></param>
        public virtual void Init(Model model)
        {
            BindModel = model;
        }

        /// <summary>
        /// 绑定事件
        /// </summary>
        /// <param name="Attribute"></param>
        /// <param name="fun"></param>
        protected void SetModel(Enum Attribute,Notifier.StandardDelegate fun)
        {
            if(BindModel!=null)
            {
                string KeyName = string.Format("{0}{1}", BindModel.GetModelName(), Attribute);
                if(FunList.ContainsKey(KeyName))
                {
                    BindModel.RemoveEventHandler(KeyName, FunList[KeyName]);
                    FunList.Remove(KeyName);
                }
                BindModel.AddEventHandler(KeyName, fun);
                FunList.Add(KeyName, fun);
            }
        }

        /// <summary>
        /// 解绑事件
        /// </summary>
        /// <param name="Attribute"></param>
        /// <param name="fun"></param>
        protected void UnSetModel(Enum Attribute, Notifier.StandardDelegate fun)
        {
            if (BindModel != null)
            {
                string KeyName = string.Format("{0}{1}", BindModel.GetModelName(), Attribute);
                if (FunList.ContainsKey(KeyName))
                {
                    BindModel.RemoveEventHandler(KeyName, FunList[KeyName]);
                    FunList.Remove(KeyName);
                }
            }
        }

        protected virtual void OnDestroy()
        {
            CleanModel();
        }

        protected virtual void ResetModel()
        {
            CleanModel();
        }

        void CleanModel()
        {
            if(BindModel!=null)
            {
                if(FunList.Count>0)
                {
                    var e = FunList.GetEnumerator();
                    while(e.MoveNext())
                    {
                        BindModel.RemoveEventHandler(e.Current.Key, e.Current.Value);
                    }
                    e.Dispose();
                }
                BindModel = null;
            }
        }
    }
}
