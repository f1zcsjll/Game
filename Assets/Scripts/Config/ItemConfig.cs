using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base;
using Config.Data;

namespace Config
{
    public class ItemConfig :BaseConfig
    {
        Dictionary<int, CItem> ItemList = new Dictionary<int, CItem>();
        public override void InitConfig()
        {
            ItemList = ReadConfigData<int, CItem>(Define.DataType.Item);
        }

        public override void UnInitConfig()
        {
            ItemList.Clear();
        }

        /// <summary>
        /// 根据ID获取物品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CItem GetItemById(int id)
        {
            if(ItemList.ContainsKey(id))
            {
                return ItemList[id];
            }
            return null;
        }

        /// <summary>
        /// 根据ID和类型获取物品某项信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public object GetItemDataById(int id,CItem.Type type)
        {
            if(ItemList.ContainsKey(id))
            {
                return ItemList[id].GetData(type);
            }
            return null;
        }
    }
}