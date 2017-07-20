using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base;
using Config.Data;
using Manager;

namespace Config
{
    public class JobConfig :BaseConfig
    {
        Dictionary<int, CJob> JobList = new Dictionary<int, CJob>();
        public override void InitConfig()
        {
            JobList = DataMgr.GetInstance().GetConfigItems<int, CJob>(Define.DataType.Job);
        }
        public override void UnInitConfig()
        {
            JobList.Clear();
        }
        /// <summary>
        /// 根据ID获取职业信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CJob GetItemById(int id)
        {
            if (JobList.ContainsKey(id))
            {
                return JobList[id];
            }
            return null;
        }

        /// <summary>
        /// 根据ID和类型获取职业某项信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public object GetItemDataById(int id, CJob.Type type)
        {
            if (JobList.ContainsKey(id))
            {
                return JobList[id].GetData(type);
            }
            return null;
        }
    }

}