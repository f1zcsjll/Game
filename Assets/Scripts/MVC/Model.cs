using System;
using System.Collections;
using System.Collections.Generic;

namespace MVC
{
    public class Model :Notifier
    {
        protected string ModelName;

        public string GetModelName()
        {
            if(string.IsNullOrEmpty(ModelName))
            {
                ModelName = GetHashCode().ToString();
            }
            return ModelName;
        }

        public bool HasModel(string name)
        {
            return HasEvent(name);
        }

        public void Refresh(Enum Attribute,params object[] arg)
        {
            RaiseEvent(string.Format("{0}{1}", GetModelName(), Attribute), arg);
        }
    }
}
