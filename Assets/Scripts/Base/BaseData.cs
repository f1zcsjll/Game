using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base
{
    public abstract class BaseData
    {
        protected Dictionary<Enum, object> DataList = new Dictionary<Enum, object>();
        public abstract BaseData ReadData(string data);
        protected abstract Define.DataType GetDataType();
        public object GetData(Enum type)
        {
            if (DataList.ContainsKey(type))
            {
                return DataList[type];
            }
            return null;
        }
    }
}