using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

namespace Logic.Game
{
    public class LoadScene : MonoBehaviour
    {

        void Start()
        {
            UIMgr.GetInstance().ShowWin(Define.UIType.WinLoading);
        }

        void Update()
        {

        }
    }
}
