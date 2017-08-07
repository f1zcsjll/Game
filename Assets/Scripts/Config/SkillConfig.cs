using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base;
using Config.Data;
using Manager;

namespace Config
{
    public class SkillConfig : BaseConfig
    {
        Dictionary<int, CSkill> SkillList = new Dictionary<int, CSkill>();
        public override void InitConfig()
        {
            SkillList = DataMgr.GetInstance().GetConfigItems<int, CSkill>(Define.DataType.Skill);
        }
        public override void UnInitConfig()
        {
            SkillList.Clear();
        }
        /// <summary>
        /// 根据ID获取技能信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CSkill GetItemById(int id)
        {
            if (SkillList.ContainsKey(id))
            {
                return SkillList[id];
            }
            return null;
        }

        /// <summary>
        /// 根据ID和类型获取技能某项信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public object GetItemDataById(int id, CSkill.Type type)
        {
            if (SkillList.ContainsKey(id))
            {
                return SkillList[id].GetData(type);
            }
            return null;
        }
    }
}