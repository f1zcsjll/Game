using Define;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public class Path
    {
        /// <summary>
        /// 读写文件路径
        /// </summary>
        public static string SaveDataPath
        {
            get { return Application.persistentDataPath+"/"; }
        }

        //一级资源路径
        static string Data = "Data/";
        static string Icon = "Icon/";
        static string Material = "Material/";
        static string Model = "Model/";
        static string Texture = "Textrue/";
        static string UI = "UI/";


        //二级资源路径
        static string Win = "Win/";

        //三级资源路径


        /// <summary>
        /// UI资源路径
        /// </summary>
        protected static Dictionary<UIType, string> UIPathList = new Dictionary<UIType, string>();
        /// <summary>
        /// 图标资源路径
        /// </summary>
        protected static Dictionary<IconType, string> IconPathList = new Dictionary<IconType, string>();
        /// <summary>
        /// 声音资源路径
        /// </summary>
        protected static Dictionary<SoundType, string> SoundPathList = new Dictionary<SoundType, string>();
        /// <summary>
        /// 图片资源路径
        /// </summary>
        protected static Dictionary<TextureType, string> TexturePathList = new Dictionary<TextureType, string>();
        /// <summary>
        /// 数据库资源路径
        /// </summary>
        protected static Dictionary<DataType, string> DataPathList = new Dictionary<DataType, string>();
        /// <summary>
        /// 材质资源路径
        /// </summary>
        protected static Dictionary<MaterialType, string> MaterialPathList = new Dictionary<MaterialType, string>();
        /// <summary>
        /// 模型资源路径
        /// </summary>
        protected static Dictionary<ModelType, string> ModelPathList = new Dictionary<ModelType, string>();

        protected static void Init()
        {
            UIPathInit();
            IconPathInit();
            SoundPathInit();
            TexturePathInit();
            DataPathInit();
            MaterialPathInit();
            ModelPathInit();
        }

        static void UIPathInit()
        {
            UIPathList.Add(UIType.WinLoading, UI + Win + "WinLoading");
        }

        static void IconPathInit()
        {

        }

        static void SoundPathInit()
        {

        }

        static void TexturePathInit()
        {

        }

        static void DataPathInit()
        {
            DataPathList.Add(DataType.Localization, Data+"localization");
            DataPathList.Add(DataType.Item, Data + "Item");
            DataPathList.Add(DataType.Job, Data + "Job");
            DataPathList.Add(DataType.Skill, Data + "Skill");
        }

        static void MaterialPathInit()
        {

        }

        static void ModelPathInit()
        {
            
        }

        protected void CleanList()
        {
            UIPathList.Clear();
            IconPathList.Clear();
            SoundPathList.Clear();
            TexturePathList.Clear();
            DataPathList.Clear();
            MaterialPathList.Clear();
            ModelPathList.Clear();
        }
    }
}
