using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Config;

namespace Manager
{
    public class ConfigMgr
    {
        private static ConfigMgr _instance;

        public ItemConfig ItemConfig = new ItemConfig();
        private ConfigMgr()
        {
            ItemConfig.InitConfig();
        }
        public static ConfigMgr GetInstance()
        {
            if (_instance == null)
                _instance = new ConfigMgr();
            return _instance;
        }
        public void CleanAllConfig()
        {
            ItemConfig.UnInitConfig();
        }
    }
}