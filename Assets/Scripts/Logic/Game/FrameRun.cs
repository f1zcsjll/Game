using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

namespace Logic.Game
{
    public class FrameRun : MonoBehaviour
    {
        void Update()
        {
            FrameMgr.GetInstance().DoUpdate();
        }

        void FixedUpdate()
        {
            FrameMgr.GetInstance().DoFixedUpdate();
        }

        void LateUpdate()
        {
            FrameMgr.GetInstance().DoLateUpdate();
        }
    }
}
