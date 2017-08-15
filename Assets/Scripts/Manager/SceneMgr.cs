using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class SceneMgr
    {
        public Camera SceneCamera;
        private GameObject EntityRoot;
        Dictionary<long, Base.BaseMotor> EntityList = new Dictionary<long, Base.BaseMotor>();

        private static SceneMgr _instance;
        private SceneMgr()
        {
            GameObject go = new GameObject("SceneCamera", typeof(Camera), typeof(GUILayer), typeof(FlareLayer));
            SceneCamera = go.GetComponent<Camera>();
            Object.DontDestroyOnLoad(SceneCamera.gameObject);
            EntityRoot = new GameObject("EntityRoot");
            Object.DontDestroyOnLoad(EntityRoot);
        }
        public static SceneMgr GetInstance()
        {
            if (_instance == null)
                _instance = new SceneMgr();
            return _instance;
        }

        /// <summary>
        /// 创建一个实体
        /// </summary>
        /// <typeparam name="T">驱动脚本</typeparam>
        /// <param name="type">实体类型</param>
        /// <param name="assettype">资源类型</param>
        /// <param name="parent">父节点物体</param>
        /// <param name="name">实体名称</param>
        /// <returns>实体ID</returns>
        public long CreateNewEntity<T>(Define.EntityType type,Define.ModelType assettype,GameObject parent=null,string name="") where T:Base.BaseMotor,new()
        {            
            long id = -1;
            AssetMgr.GetInstance().LoadAsset(PathMgr.GetInstance().GetPath(assettype), true, true, () =>
            {
                if (AssetMgr.GetInstance().GetAsset() != null)
                {
                    GameObject go;
                    go = GameObject.Instantiate(AssetMgr.GetInstance().GetAsset(), parent == null ? EntityRoot.transform : parent.transform) as GameObject;
                    id = go.GetHashCode() >= 0 ? go.GetHashCode() * 10 : -go.GetHashCode() * 10 + 1;
                    string goname = name == "" ? (id + "_" + assettype.ToString()) : name;
                    go.name = goname;                   
                    go.AddComponent(typeof(T));
                    go.GetComponent<T>().InitEntity(id, goname, type);
                    EntityList.Add(id, go.GetComponent<T>());
                }
                else
                {
                    Debug.LogError("Create Failed!");
                    return;
                }
            });
            return id;
        }

        /// <summary>
        /// 根据ID获取场景中实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Base.BaseMotor GetMotor(long id)
        {
            if(EntityList.ContainsKey(id))
            {
                return EntityList[id];
            }
            else
            {
                return null;
            }
        }
    }
}