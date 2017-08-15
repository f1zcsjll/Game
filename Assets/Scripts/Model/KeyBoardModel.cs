using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;
using System.IO;
using System;

namespace Model
{
    public class KeyInfo
    {
        /// <summary>
        /// 按键类型
        /// </summary>
        public KeyBoardType Type;
        /// <summary>
        /// 按键列表
        /// </summary>
        public List<KeyCode> KeyList = new List<KeyCode>();
        /// <summary>
        /// 按键信息
        /// </summary>
        public string Info;
        /// <summary>
        /// 按键冷却时间
        /// </summary>
        public float KeyColdTime;

        bool press;
        float coldtime = 0f;
        /// <summary>
        /// 按键是否被按下
        /// </summary>
        public bool isPress
        {
            get
            {
                if(Manager.KeyBoardMgr.GetInstance().Model.IsSetting==true)
                {
                    return false;
                }
                if(coldtime>0)
                {
                    return false;
                }
                press = true;
                for(int i=0;i<KeyList.Count;i++)
                {
                    if(!Input.GetKey(KeyList[i]))
                    {
                        press = false;
                        break;
                    }
                }
                if(press)
                {
                    coldtime = KeyColdTime;
                }
                return press;
            }
        }

        /// <summary>
        /// 更新冷却时间
        /// </summary>
        public void UpdateColdTime()
        {
            if(coldtime>0)
            {
                coldtime -= Time.deltaTime;
            }
        }
    }

    public class KeyBoardModel:MVC.Model
    {
        /// <summary>
        /// 按键字典
        /// </summary>
        Dictionary<KeyBoardType, KeyInfo> KeySetList = new Dictionary<KeyBoardType, KeyInfo>();
        /// <summary>
        /// 保存设定路径
        /// </summary>
        string ConfigPath = Tools.Path.SaveDataPath + "Key.config";
        /// <summary>
        /// 是否在按键设定中
        /// </summary>
        public bool IsSetting = false;

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            ResetKeySetting();
            ReadKeyConfig();
            if(!File.Exists(ConfigPath))
            {
                SaveKeySetting();
            }
            Manager.FrameMgr.GetInstance().RegisterUpdate(Update);
        }

        /// <summary>
        /// 更新按键冷却
        /// </summary>
        public void Update()
        {
            var e = KeySetList.GetEnumerator();
            while(e.MoveNext())
            {
                e.Current.Value.UpdateColdTime();
            }
            e.Dispose();
        }

        /// <summary>
        /// 获取按键列表
        /// </summary>
        /// <returns></returns>
        public Dictionary<KeyBoardType,KeyInfo> GetKeyList()
        {
            return KeySetList;
        }

        /// <summary>
        /// 设定按键
        /// </summary>
        /// <param name="type"></param>
        /// <param name="keylist"></param>
        public void SetKey(KeyBoardType type,List<KeyCode> keylist)
        {
            if(KeySetList.ContainsKey(type))
            {
                KeySetList[type].KeyList.Clear();
                for(int i=0;i<keylist.Count;i++)
                {
                    KeySetList[type].KeyList.Add(keylist[i]);
                }
            }
        }

        /// <summary>
        /// 获取某个按键信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public KeyInfo GetKeyInfo(KeyBoardType type)
        {
            if(KeySetList.ContainsKey(type))
            {
                return KeySetList[type];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 按键是否被按下
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool KeyIsPress(KeyBoardType type)
        {
            if(KeySetList==null||!KeySetList.ContainsKey(type)||KeySetList[type].KeyList.Count==0)
            {
                return false;
            }
            else
            {
                return KeySetList[type].isPress;
            }
        }

        /// <summary>
        /// 重置按键设定
        /// </summary>
        public void ResetKeySetting()
        {
            Refresh(Define.EventType.KeySetting);
        }

        /// <summary>
        /// 读取玩家案件设定
        /// </summary>
        public void ReadKeyConfig()
        {
            if(File.Exists(ConfigPath))
            {
                string[] info = File.ReadAllLines(ConfigPath, System.Text.Encoding.UTF8);
                for(int i=1;i<info.Length;i++)
                {
                    if(!string.IsNullOrEmpty(info[i]))
                    {
                        string[] keyset = info[i].Split(',');
                        if(KeySetList.ContainsKey(ChangeStrToKeyBoardType(keyset[0])))
                        {
                            KeySetList[ChangeStrToKeyBoardType(keyset[0])].KeyList.Clear();
                            string[] keystr = keyset[1].Split('+');
                            for(int j=0;j<keystr.Length;j++)
                            {
                                KeySetList[ChangeStrToKeyBoardType(keyset[0])].KeyList.Add(ChangeStrToKeyCode(keystr[j]));
                            }
                        }
                    }
                }
                Refresh(Define.EventType.KeySetting);
            }
            else
            {
                ResetKeySetting();
                SaveKeySetting();
            }
        }

        /// <summary>
        /// 保存按键设定
        /// </summary>
        public void SaveKeySetting()
        {
            if(KeySetList.Count>0)
            {
                StreamWriter sw = new StreamWriter(ConfigPath, false, System.Text.Encoding.UTF8);
                string strLine = "KeyBoardType,KeyCode";
                sw.WriteLine(strLine);
                var e = KeySetList.GetEnumerator();
                while(e.MoveNext())
                {
                    if(e.Current.Value.KeyList.Count>0)
                    {
                        KeyInfo info = e.Current.Value;
                        strLine = info.Type.ToString() + ",";
                        for(int i=0;i<info.KeyList.Count;i++)
                        {
                            if (i == 0)
                                strLine += info.KeyList[i];
                            else
                                strLine += ("+" + info.KeyList[i]);
                        }
                        sw.WriteLine(strLine);
                    }
                }
                sw.Close();
                Refresh(Define.EventType.KeySetting);
            }
        }

        /// <summary>
        /// 将字符串转换为KeyCode
        /// </summary>
        /// <param name="keystr"></param>
        /// <returns></returns>
        public KeyCode ChangeStrToKeyCode(string keystr)
        {
            return (KeyCode)Enum.Parse(typeof(KeyCode), keystr);
        }

        /// <summary>
        /// 将字符串转换为KeyBoardType
        /// </summary>
        /// <param name="keystr"></param>
        /// <returns></returns>
        public KeyBoardType ChangeStrToKeyBoardType(string keystr)
        {
            return (KeyBoardType)Enum.Parse(typeof(KeyBoardType), keystr);
        }
    }
}