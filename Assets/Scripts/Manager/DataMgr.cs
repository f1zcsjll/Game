using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

namespace Manager
{
    public class DataMgr
    {       
        private static DataMgr _instance;

        /// <summary>
        /// 数据管理器
        /// </summary>
        /// <returns></returns>
        public static DataMgr GetInstance()
        {
            if (_instance == null)
                _instance = new DataMgr();
            return _instance;
        }
       
        /// <summary>
        /// 保存玩家数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        public void SavePrefsData<T>(string name,object data)
        {
            if (typeof(T) == typeof(int))
                PlayerPrefs.SetInt(name, int.Parse(data.ToString()));
            else if (typeof(T) == typeof(float))
                PlayerPrefs.SetFloat(name, float.Parse(data.ToString()));
            else if (typeof(T) == typeof(string))
                PlayerPrefs.SetString(name, (string)data);
            else
                Debug.LogError("SavePrefsData Error!");
        }

        /// <summary>
        /// 获取玩家数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public T GetPrefsData<T>(string name)
        {
            T result;
            if (PlayerPrefs.HasKey(name))
            {
                if (typeof(T) == typeof(int))
                {
                    return result = (T)(object)PlayerPrefs.GetInt(name);
                }
                else if (typeof(T) == typeof(float))
                {
                    return result = (T)(object)PlayerPrefs.GetFloat(name);
                }
                else if (typeof(T) == typeof(string))
                {
                    return result = (T)(object)PlayerPrefs.GetString(name);
                }
            }
            Debug.LogError("GetPrefsData Error!");                
            return default(T);
        }

        public void SaveAllPrefsData()
        {
            PlayerPrefs.Save();
        }
       
        /// <summary>
        /// 清除玩家某项数据
        /// </summary>
        /// <param name="name"></param>
        public void CleanPrefsData(string name)
        {
            PlayerPrefs.DeleteKey(name);
        }

        /// <summary>
        /// 清除所有玩家数据
        /// </summary>
        public void CleanAllPrefsData()
        {
            PlayerPrefs.DeleteAll();
        }

        /// <summary>
        /// 读取配置信息
        /// </summary>
        /// <typeparam name="N">主键类型</typeparam>
        /// <typeparam name="T">配置表数据</typeparam>
        /// <param name="type">配置类型</param>
        /// <returns></returns>
        public Dictionary<N,T> GetConfigItems<N,T>(DataType type) where T:Base.BaseData,new()
        {
            Dictionary<N, T> list = new Dictionary<N, T>();
            string[] data=new string[0];
            AssetMgr.GetInstance().LoadAsset(PathMgr.GetInstance().GetPath(type), false, false, () =>
            {
                if (AssetMgr.GetInstance().GetAsset() == null)
                    return;
                data = AssetMgr.GetInstance().GetAsset().ToString().Split('\n');
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = data[i].Replace("\r", "");
                }
            });
            if (data.Length==0)
                return null;
            else if (data.Length == 1)
                return list;
            else
            {                
                for (int i = 1; i < data.Length; i++)
                {
                    if(data[i].StartsWith("#"))
                    {
                        continue;
                    }
                    T temp=new T();
                    temp.ReadData(data[i]);
                    object key = null;
                    if(typeof(N)==typeof(int))
                    {
                        key = int.Parse(data[i].Split(',')[0]);
                    }
                    else if (typeof(N) == typeof(long))
                    {
                        key = long.Parse(data[i].Split(',')[0]);
                    }
                    else
                    {
                        key = data[i].Split(',')[0];
                    }
                    list.Add((N)key, temp);
                }
                return list;
            }
        }
    }
}
