using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

namespace Manager
{
    public class SoundMgr
    {
        private static SoundMgr _instance;
        private GameObject SoundRoot;
        private Dictionary<SoundType, AudioSource> SoundList = new Dictionary<SoundType, AudioSource>();

        public float Volume = 1.0f;
        private SoundMgr()
        {
            SoundRoot = new GameObject("SoundRoot");
            SoundRoot.AddComponent<AudioListener>();
            UnityEngine.Object.DontDestroyOnLoad(SoundRoot);
        }
        public static SoundMgr GetInstance()
        {
            if (_instance == null)
                _instance = new SoundMgr();
            return _instance;
        }

        /// <summary>
        /// 创建声音
        /// </summary>
        /// <param name="type">声音类型</param>
        /// <param name="isplay">是否立即播放</param>
        /// <param name="isloop">是否循环播放</param>
        /// <param name="volume">音量</param>
        public void CreateSound(SoundType type,bool isplay=false,bool isloop=false,float volume=1.0f)
        {
            if (!HasCreate(type))
            {
                GameObject temp = new GameObject(type.ToString());
                temp.transform.parent=SoundRoot.transform;
                AudioSource tempsource = temp.AddComponent<AudioSource>();
                AssetMgr.GetInstance().LoadAsset("", true, true, () => { tempsource.clip = (AudioClip)AssetMgr.GetInstance().GetAsset(); });
                tempsource.volume = Volume*volume;
                tempsource.Stop();
                SoundList.Add(type, tempsource);
                if(isplay)
                {
                    PlaySound(type);
                }
            }
        }

        /// <summary>
        /// 声音是否已创建
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool HasCreate(SoundType type)
        {
            return SoundList.ContainsKey(type);
        }

        /// <summary>
        /// 播放声音
        /// </summary>
        /// <param name="type"></param>
        /// <param name="isloop"></param>
        /// <param name="delaytime">延迟播放时间</param>
        public void PlaySound(SoundType type,bool isloop=false,ulong delaytime=0)
        {
            if(SoundList.ContainsKey(type))
            {
                SoundList[type].loop = isloop;
                SoundList[type].Play(delaytime);
            }
        }

        /// <summary>
        /// 是否正在播放
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool IsPlaying(SoundType type)
        {
            return SoundList[type].isPlaying;
        }

        /// <summary>
        /// 设定音量
        /// </summary>
        /// <param name="type"></param>
        /// <param name="volume"></param>
        public void SetVolume(SoundType type,float volume)
        {
            SoundList[type].volume = Volume * volume;
        }

        /// <summary>
        /// 设定循环
        /// </summary>
        /// <param name="type"></param>
        /// <param name="isloop"></param>
        public void SetLoop(SoundType type,bool isloop)
        {
            SoundList[type].loop = isloop;
        }

        /// <summary>
        /// 停止播放
        /// </summary>
        /// <param name="type"></param>
        public void StopSound(SoundType type)
        {
            if(SoundList.ContainsKey(type))
            {
                SoundList[type].Stop();
            }
        }

        /// <summary>
        /// 停止所有声音播放
        /// </summary>
        public void StopAllSound()
        {
            var e = SoundList.GetEnumerator();
            while(e.MoveNext())
            {
                e.Current.Value.Stop();
            }
        }

        /// <summary>
        /// 删除声音物体
        /// </summary>
        /// <param name="type"></param>
        public void DeleteSound(SoundType type)
        {
            if(SoundList.ContainsKey(type))
            {
                StopSound(type);
                GameObject.Destroy(SoundList[type].gameObject);
                AssetMgr.GetInstance().CleanAssetPool("");
                SoundList.Remove(type);
            }
        }

        /// <summary>
        /// 清除所有声音
        /// </summary>
        public void DeleteAllSound()
        {
            var e = SoundList.GetEnumerator();
            while (e.MoveNext())
            {
                e.Current.Value.Stop();
                GameObject.Destroy(e.Current.Value.gameObject);
                AssetMgr.GetInstance().CleanAssetPool("");
            }
            e.Dispose();
            SoundList.Clear();
        }
    }
}
