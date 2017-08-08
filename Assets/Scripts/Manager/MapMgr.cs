using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class MapMgr
    {
        static private MapMgr _instance;
        static public MapMgr GetInstance()
        {
            if (_instance == null)
                _instance = new MapMgr();
            return _instance;
        }
    }
}