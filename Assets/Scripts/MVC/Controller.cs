using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public abstract class Controller :Notifier
    {
        public virtual void Init()
        {
            RegistEventHandler();
        }

        public abstract void RegistEventHandler();
    }
}
