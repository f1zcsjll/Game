using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Manager
{
    public class KeyBoardMgr
    {
        GameObject KeyBoardRoot;
        static private KeyBoardMgr _instance;
        static public KeyBoardMgr GetInstance()
        {
            if (_instance == null)
                _instance = new KeyBoardMgr();
            return _instance;
        }
        private KeyBoardMgr()
        {
            KeyBoardRoot = new GameObject("KeyBoardRoot", typeof(Logic.Game.KeyBoardControl));
            UnityEngine.Object.DontDestroyOnLoad(KeyBoardRoot);
        }

        public Model.KeyBoardModel Model = new Model.KeyBoardModel();

        /// <summary>
        /// 获取当前按下的按键键值
        /// </summary>
        /// <returns></returns>
        public static KeyCode GetKeyDownCode()
        {
            if (Input.anyKeyDown)
            {
                foreach (KeyCode keycode in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(keycode))
                    {
                        return keycode;
                    }
                }
            }
            return KeyCode.None;
        }
    }
}