using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;
using Tools;

namespace Manager
{
    public class PathMgr:Path
    {
        private static PathMgr _instance;
        private PathMgr()
        {
            Path.Init();
        }
        public static PathMgr GetInstance()
        {
            if (_instance == null)
                _instance = new PathMgr();
            return _instance;
        }

        /// <summary>
        /// 获取UI资源路径
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetPath(UIType type)
        {
            if(UIPathList.ContainsKey(type))
            {
                return UIPathList[type];
            }
            return "";
        }

        /// <summary>
        /// 获取数据库资源路径
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetPath(DataType type)
        {
            if (DataPathList.ContainsKey(type))
            {
                return DataPathList[type];
            }
            return "";
        }

        /// <summary>
        /// 获取图标资源路径
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetPath(IconType type)
        {
            if (IconPathList.ContainsKey(type))
            {
                return IconPathList[type];
            }
            return "";
        }

        /// <summary>
        /// 获取声音资源路径
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetPath(SoundType type)
        {
            if (SoundPathList.ContainsKey(type))
            {
                return SoundPathList[type];
            }
            return "";
        }

        /// <summary>
        /// 获取模型资源路径
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetPath(ModelType type)
        {
            if (ModelPathList.ContainsKey(type))
            {
                return ModelPathList[type];
            }
            return "";
        }

        /// <summary>
        /// 获取图片资源路径
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetPath(TextureType type)
        {
            if (TexturePathList.ContainsKey(type))
            {
                return TexturePathList[type];
            }
            return "";
        }
        
        /// <summary>
        /// 获取材质资源路径
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetPath(MaterialType type)
        {
            if (MaterialPathList.ContainsKey(type))
            {
                return MaterialPathList[type];
            }
            return "";
        }

        /// <summary>
        /// 清除列表记录
        /// </summary>
        public void CleanPathList()
        {
            CleanList();
        }
    }
}