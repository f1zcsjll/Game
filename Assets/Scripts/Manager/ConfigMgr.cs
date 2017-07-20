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
        public JobConfig JobConfig = new JobConfig();

        private ConfigMgr()
        {
            ItemConfig.InitConfig();
            JobConfig.InitConfig();
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
            JobConfig.InitConfig();
        }
    }
}