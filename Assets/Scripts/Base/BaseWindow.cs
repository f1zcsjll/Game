using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MVC;
using Define;
using UnityEngine.UI;
using UnityEngine.Events;
using Manager;

namespace Base
{
    public abstract class BaseWindow : View
    {
        [SerializeField]
        protected Button CloseButton;

        protected virtual void Start()
        {
            if(CloseButton!=null)
            {
                CloseButton.onClick.AddListener(delegate() { OnClickCloseButton(); });
            }
        }

        protected virtual void OnClickCloseButton()
        {
            UIMgr.GetInstance().CloseWin(GetUIType());
        }

        protected abstract UIType GetUIType();
    }
}