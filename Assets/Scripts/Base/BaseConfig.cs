using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;
using Manager;

namespace Base
{
    public abstract class BaseConfig
    {
        protected Dictionary<N, T> ReadConfigData<N, T>(DataType type) where T : Base.BaseData, new()
        {
            return DataMgr.GetInstance().GetConfigItems<N, T>(type);
        }

        public abstract void InitConfig();

        public abstract void UnInitConfig();
    }
}
