using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class AssetMgr
    {
        private static AssetMgr _instance;

        private Dictionary<string, UnityEngine.Object> AssetPool;

        UnityEngine.Object Obj;

        private AssetMgr()
        {
            if (AssetPool == null)
                AssetPool = new Dictionary<string, UnityEngine.Object>();
            CleanAssetPool();
        }
        /// <summary>
        /// 资源加载管理器
        /// </summary>
        /// <returns></returns>
        public static AssetMgr GetInstance()
        {
            if (_instance == null)
                _instance = new AssetMgr();
            return _instance;
        }

        /// <summary>
        /// 加载资源
        /// </summary>
        /// <param name="Path">资源路径</param>
        /// <param name="isAsync">是否异步加载</param>
        /// <param name="isPool">是否加入对象池</param>
        /// <param name="fallback">完成加载后回调</param>
        public void LoadAsset(string Path,bool isAsync=false,bool isPool=false,Action fallback=null)
        {
            Obj = null;
            if(AssetPool.ContainsKey(Path))
            {
                Obj = AssetPool[Path];
                if(!isPool)
                {
                    AssetPool.Remove(Path);
                }
                if(fallback!=null)
                {
                    fallback();
                }
                return;
            }
            if(isAsync)
            {
                ResourceRequest req = Resources.LoadAsync(Path);
                if(req.isDone)
                {
                    Obj = req.asset;
                    if (Obj != null)
                    {
                        if (isPool)
                        {
                            AssetPool.Add(Path, Obj);
                        }
                        if (fallback != null)
                        {
                            fallback();
                        }
                        return;
                    }
                    else
                    {
                        Debug.LogError("LoadAsset Failed");
                        return;
                    }
                }
            }
            else
            {
                Obj = Resources.Load(Path);
                if(Obj!=null)
                {
                    if(isPool)
                    {
                        AssetPool.Add(Path, Obj);
                    }
                    if(fallback!=null)
                    {
                        fallback();
                    }
                    return;
                }
                else
                {
                    Debug.LogError("LoadAsset Failed");
                    return;
                }
            }
        }

        /// <summary>
        /// 获取加载完的资源
        /// </summary>
        /// <returns></returns>
        public UnityEngine.Object GetAsset()
        {
            return Obj;
        }

        /// <summary>
        /// 清理资源池
        /// </summary>
        public void CleanAssetPool()
        {
            if(AssetPool!=null)
                AssetPool.Clear();
        }
    }
}
