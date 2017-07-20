using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base;

namespace Config.Data
{
    public class CItem :BaseData
    {
        public enum Type
        {
            ID=0,
            Name,
            Type,
            AddHp,       
            AddMp,

        };
        
        protected override Define.DataType GetDataType()
        {
            return Define.DataType.Item;
        }
        public override BaseData ReadData(string data)
        {
            string[] temp = data.Split(',');
            for (int i = 0; i < temp.Length;i++)
            {
                DataList.Add((Type)i, temp[i]);
            }
            return this;
        }
    }
}