using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base;

namespace Config.Data
{
    public class CJob :BaseData
    {
        public enum Type
        {
            ID,
            Name,
            Type,
            Hp,
            Mp,

        }

        public override BaseData ReadData(string data)
        {
            string[] temp = data.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                DataList.Add((Type)i, temp[i]);
            }
            return this;
        }
    }
}